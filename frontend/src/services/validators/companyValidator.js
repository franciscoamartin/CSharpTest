export default function ValidateCompany(company) {
  validateCNPJ(company.cnpj);
  validateTradingName(company.tradingName);
}

function validateCNPJ(cnpj) {
  cnpj = cnpj.replace(/[^\d]+/g, '');

  if (cnpj.length != 14) throw new Error('CNPJ é inválido.');
}
function validateTradingName(TradingName) {
  if (TradingName.length < 3) throw new Error('Nome fantasia é inválido');
}
