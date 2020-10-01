import InputMask from 'react-input-mask';
import React from 'react';

export default function EntityType(props) {
  function clearCPFInput() {
    props.setRG('');
    props.setDocumentNumber('');
    props.setBirthDate('');
  }

  return (
    <>
      <select
        className="select-person"
        onChange={(e) => {
          clearCPFInput();
          const stringValue = e.target.value;
          props.setDocumentType(Number(stringValue));
        }}
      >
        <option value={1}>CNPJ</option>
        <option value={2}>CPF</option>
      </select>
      <div className="column entity-type-container">
        {props.documentType === 1 ? (
          <InputMask
            mask="99.999.999/9999-99"
            value={props.documentNumber}
            onChange={(e) => props.setDocumentNumber(e.target.value)}
            placeholder="CNPJ"
          >
            {(inputProps) => <input {...inputProps} />}
          </InputMask>
        ) : (
            <>
              <InputMask
                mask="999.999.999-99"
                value={props.documentNumber}
                onChange={(e) => props.setDocumentNumber(e.target.value)}
                placeholder="CPF"
              >
                {(inputProps) => <input {...inputProps} />}
              </InputMask>
              <input
                placeholder="RG"
                value={props.rg}
                onChange={(e) => props.setRG(e.target.value)}
              />
              <input
                placeholder="Data de nascimento"
                type="date"
                value={props.birthDate}
                onChange={(e) => props.setBirthDate(e.target.value)}
              />
            </>
          )}
      </div>
    </>
  );
}
