import React, { useState, useEffect } from 'react';
import MaterialTable from 'material-table';

export default function CompaniesTable(props) {
  const [columns, setColumns] = React.useState([
    {
      title: 'Nome Fantasia',
      field: 'tradingName',
    },
    {
      title: 'UF',
      field: 'uf',
    },
    { title: 'CNPJ', field: 'cnpj' },
  ]);

  function handleOnRowAdd() {}
  function handleOnRowUpdate() {}
  function handleOnRowDelete() {}

  return (
    <MaterialTable
      title="Selecione uma empresa para ver seus fornecedores"
      columns={columns}
      data={props.companies}
      actions={[
        {
          icon: 'check_circle_outline',
          tooltip: 'Selecionar empresa para ver seus fornecedores',
          onClick: (event, rowData) => {
            console.log(rowData);
            alert(`lina linda`);
          },
        },
      ]}
    />
  );
}
