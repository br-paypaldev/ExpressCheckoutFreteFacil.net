namespace Controllers {
	using System;
	using System.Collections.Generic;
	using System.Web;
	using System.Web.Mvc;
	using PayPal;
	using PayPal.Enum;
	using PayPal.ExpressCheckout;
	using PayPal.FreteFacil;
	using Models;

	[HandleError]
	public class HomeController : Controller {
		public ActionResult Index() {
			string origem = "14400-000";
			string destino = "90619-900";
			double peso = 1;
			int largura = 15;
			int altura = 2;
			int comprimento = 30;
			double freteECT;
			double freteFacil;
			
			//Preço do frete da ECT
			CalcPrecoPrazoWS wsECT = new CalcPrecoPrazoWS();
			freteECT = Double.Parse( wsECT.CalcPrecoPrazo(
				"","","40010",
				origem,
				destino,
				peso.ToString(),
				1,
				comprimento,
				altura,
				largura,
				0,"n",0,"n"
			).Servicos[0].Valor );
			
			//Preço do frete utilizando PayPal Frete Fácil
			FreteFacilApi wsFreteFacil = PayPalApiFactory.instance.FreteFacil();
			freteFacil = wsFreteFacil.getPreco(
				origem,
				destino,
				largura,
				altura,
				comprimento,
				peso.ToString()
			);
			
			SetExpressCheckoutOperation SetExpressCheckout = PayPalApiFactory.instance.ExpressCheckout(
				"neto_1306507007_biz_api1.gmail.com",
				"1306507019",
				"Al8n1.tt9Sniswt8UZvcamFvsXYEAegNpyX63HRdtqJVff7rESMSQ3qN"
			).SetExpressCheckout(
				"http://dominio.com/url/de/sucesso",
				"http://dominio.com/url/de/cancelamento"
			);
			
			SetExpressCheckout.LocaleCode = LocaleCode.BRAZILIAN_PORTUGUESE;
			SetExpressCheckout.CurrencyCode = CurrencyCode.BRAZILIAN_REAL;
			SetExpressCheckout.HeaderImage = "https://cms.paypal.com/cms_content/US/en_US/images/developer/PP_X_Final_logo_vertical_rgb.gif";
			SetExpressCheckout.BrandName = "Nome da minha loja";
			SetExpressCheckout.SurveyEnable = true;
			SetExpressCheckout.SurveyQuestion = "Onde ficou sabendo de nossa loja?";
			SetExpressCheckout.SurveyChoice = new string[]{
				"Um amigo me contou",
				"Mecanismo de pesquisa",
				"Anúncio em website",
				"Outros"
			};
			
			SetExpressCheckout.PaymentRequest(0).addItem( "Produto de Teste 1" ,  1 , 10 , "Descrição do produto 1" );
			SetExpressCheckout.PaymentRequest(0).addItem( "Produto de Teste 2" ,  2 , 11 , "Descrição do produto 2" );
			SetExpressCheckout.PaymentRequest(0).addItem( "Produto de Teste 3" ,  3 , 12 , "Descrição do produto 3" );
			SetExpressCheckout.PaymentRequest(0).ShippingAmount = freteECT; //Valor calculado utilizando o webservice do Correios
			SetExpressCheckout.PaymentRequest(0).ShippingDiscountAmount = freteFacil - freteECT; //Diferença entre os valores
			
			SetExpressCheckout.sandbox().execute();
			
			return Redirect( SetExpressCheckout.RedirectUrl );
		}
	}
}