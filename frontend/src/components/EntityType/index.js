import InputMask from 'react-input-mask';
import React, { Fragment } from 'react';

export default function EntityType(props) {
  return (
    <>
      {props.documentType == 1 ? (
        <InputMask
          mask="99.999.999/9999-99"
          value={props.documentNumber}
          onChange={(e) => props.setDocumentNumber(e.target.value)}
          placeholder="CNPJ"
        ></InputMask>
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
    </>
  );
}
