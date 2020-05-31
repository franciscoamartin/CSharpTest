import React, { useState, useEffect } from 'react';
import MaterialTable from 'material-table';
import swal from 'sweetalert';
import './styles.css';
import * as companyService from '../../services/companyServices';

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

  function handleOnRowUpdate() {}
  async function handleDelete(event, rowData) {
    const accepted = await swal(
      'Tem que certeza deseja deletar essa empresa?',
      '',
      'info'
    );
    if (accepted) {
      try {
        await companyService.deleteCompany(rowData.id);
        const filteredCompanies = props.companies.filter(
          (c) => c.id != rowData.id
        );
        props.setCompanies(filteredCompanies);
      } catch (error) {}
    }
  }

  return (
    <section>
      <MaterialTable
        title="Selecione uma empresa para ver seus fornecedores"
        columns={columns}
        data={props.companies}
        actions={
          props.setCompanySelected
            ? [
                {
                  icon: 'check_circle_outline',
                  tooltip: 'Selecionar empresa para ver seus fornecedores',
                  onClick: (event, rowData) => {
                    props.setCompanySelected(rowData);
                    swal.close();
                  },
                },
              ]
            : [
                {
                  icon: 'edit',
                  tooltip: 'Editar',
                  onClick: handleOnRowUpdate,
                },
                {
                  icon: 'delete',
                  tooltip: 'Deletar',
                  onClick: handleDelete,
                },
              ]
        }
      />
    </section>
  );
}
