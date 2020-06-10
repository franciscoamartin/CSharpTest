export default function Validate(data) {
  if (data.document.type === 1) validateCNPJ(data.document.number);
  else {
    validateBirthDate(data.birthDate);
    validateCPF(data.document.number);
  }

  validateName(data.name);
  validateTelephones(data.telephones);
}

function validateCNPJ(cnpj) {
  cnpj = cnpj.replace(/[^\d]+/g, '');

  if (cnpj.length !== 14) throw new Error('CNPJ é inválido.');
}

function validateCPF(cpf) {
  cpf = cpf.replace('.', '').replace('.', '').replace('-', '');
  if (cpf.length !== 11) throw new Error('CPF é inválido.');
}

function validateName(name) {
  if (name.length < 3) throw new Error('Nome é inválido');
}

function validateBirthDate(birthDate) {
  if (!birthDate) {
    throw new Error('Data inválida');
  }
}

function validateTelephones(telephones) {
  telephones.forEach((telephone) => {
    if (!isTelephoneValid(telephone.number))
      throw new Error('Telefone inválido.');
  });
}

export function isTelephoneValid(telephone) {
  const formattedTelephone = telephone
    .replace('(', '')
    .replace(')', '')
    .replace('-', '')
    .replace(' ', '')
    .replace(' ', '');
  return (
    (formattedTelephone.length === 13 || formattedTelephone.length === 14) &&
    telephoneMatchesRegex(telephone)
  );
}

function telephoneMatchesRegex(telephone) {
  debugger;
  return telephone.match(/(\+\d{2})(\s)?(\()?\d{2}(\)?)(\s)?\d{4,5}(-)?\d{4}/g);
}
