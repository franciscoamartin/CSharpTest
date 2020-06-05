import React, { useState, useEffect } from 'react';
import MaterialTable from 'material-table';
import swal from 'sweetalert';
import * as companyService from '../../../services/companyServices';
import showModal from '../../LoadingModal';
import './styles.css';

export default function CompaniesTable({
  companies,
  setCompanies,
  setCompanySelected,
}) {
  const [columns, setColumns] = React.useState([
    {
      title: 'Nome Fantasia',
      field: 'tradingName',
    },
    {
      title: 'UF',
      field: 'uf',
    },
    { title: 'CNPJ', field: 'cnpj', editable: 'never' },
  ]);

  async function handleUpdate(newData, oldData) {
    const accepted = await swal(
      'Tem certeza que deseja editar essa empresa?',
      '',
      'info'
    );
    if (accepted) {
      try {
        showModal();
        const dataToSend = {
          id: oldData.id,
          uf: newData.uf,
          tradingName: newData.tradingName,
        };
        await companyService.updateCompany(dataToSend);
        swal('Empresa alterada com sucesso', '', 'success');
        getAll();
      } catch (error) {
        swal('Empresa não foi alterada', '', 'error');
      }
    }
  }

  async function handleDelete(event, rowData) {
    const accepted = await swal(
      'Tem certeza que deseja deletar essa empresa?',
      '',
      'info'
    );
    if (accepted) {
      try {
        showModal();
        await companyService.deleteCompany(rowData.id);
        const filteredCompanies = companies.filter((c) => c.id != rowData.id);
        setCompanies(filteredCompanies);
        swal('Empresa deletada com sucesso', '', 'success');
      } catch (error) {
        swal('Empresa não foi deletada', '', 'error');
      }
    }
  }

  async function getAll() {
    const companiesFound = await companyService.getAllCompanies();
    setCompanies(companiesFound);
  }

  return (
    <div className="company-table-container">
      <MaterialTable
        options={{ search: false }}
        title="Empresas"
        columns={columns}
        data={companies}
        editable={{
          onRowUpdate: handleUpdate,
        }}
        actions={
          setCompanySelected
            ? [
                {
                  icon: 'check_circle_outline',
                  tooltip: 'Selecionar empresa para ver seus fornecedores',
                  onClick: (event, rowData) => {
                    setCompanySelected(rowData);
                    swal.close();
                  },
                },
              ]
            : [
                {
                  icon: 'delete',
                  tooltip: 'Deletar',
                  onClick: handleDelete,
                },
              ]
        }
      />
    </div>
  );
}
