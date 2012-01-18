namespace Models {
	using System.ComponentModel;
	using System.Web.Services;
	using System.Web.Services.Description;
	using System.Web.Services.Protocols;
	using System.Xml.Serialization;

	[WebServiceBinding(
		Name="WSFretesPayPalPort",
		Namespace="https://ff.paypal-brasil.com.br/FretesPayPalWS"
	)]
	public partial class FreteFacilWS : SoapHttpClientProtocol {
		public FreteFacilWS() {
			this.Url = "https://ff.paypal-brasil.com.br/FretesPayPalWS/WSFretesPayPal";			
		}

		[SoapDocumentMethodAttribute("",
			RequestNamespace="https://ff.paypal-brasil.com.br/FretesPayPalWS",
			ResponseNamespace="https://ff.paypal-brasil.com.br/FretesPayPalWS",
			ParameterStyle=SoapParameterStyle.Wrapped,
			Use=SoapBindingUse.Encoded
		)]
        [return: XmlElementAttribute("return")]
        public double getPreco(
			string cepOrigem,
			string cepDestino,
			int largura,
			int altura,
			int profundidade,
			string peso ) {
			
			object[] results = this.Invoke(
				"getPreco", new object[] {
					cepOrigem,
					cepDestino,
					largura,
					altura,
					profundidade,
					peso
			} );
			
			return ((double)(results[0]));
		}
	}
}