<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<PayPalFreteFacil.Models.Shipping>" %>
<!DOCTYPE html>
<html>
<head>
<meta charset="UTF-8" />
<title>Code Sample - PayPal Frete Fácil</title>
<meta name="description" content="Exemplo de integração com PayPal Frete Fácil e C#" />
</head>
<body>
<h1>PayPal Frete Fácil</h1>
<dl>
<dt>Valor do Frete: R$ <strong><%=Model.Value%></strong></dt>
<dd><strong>Origem:</strong> 14400-000</dd>
<dd><strong>Destino:</strong> 90619-900</dd>
<dd><strong>Largura:</strong> 15</dd>
<dd><strong>Altura:</strong> 2</dd>
<dd><strong>Profundidade:</strong> 30</dd>
<dd><strong>Peso:</strong> 1</dd>
</dl>
</body>
</html>


