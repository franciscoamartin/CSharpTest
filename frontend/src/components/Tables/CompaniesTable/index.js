import React, { useState } from 'react';
import MaterialTable from 'material-table';
import swal from 'sweetalert';
import * as companyService from '../../../services/companyServices';
import showModal from '../../LoadingModal';
import './styles.css';
import showModalError from '../../../services/showModalError';

export default function CompaniesTable({
  companies,
  setCompanies,
  setCompanySelected,
}) {
  const [columns] = useState([
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
      showModalError(error, 'Empresa não foi alterada');
    }
  }

  async function handleDelete(rowData) {
    const accepted = await swal(
      'Tem certeza que deseja deletar essa empresa?',
      '',
      'info'
    );
    if (accepted) {
      try {
        showModal();
        await companyService.deleteCompany(rowData.id);
        const filteredCompanies = companies.filter((c) => c.id !== rowData.id);
        setCompanies(filteredCompanies);
        swal('Empresa deletada com sucesso', '', 'success');
      } catch (error) {
        showModalError(error, 'Empresa não foi deletada');
      }
    }
  }

  async function getAll() {
    try {
      const companiesFound = await companyService.getAllCompanies();
      setCompanies(companiesFound);
    } catch (error) {
      showModalError(error, 'não foi possível realizar a busca');
    }
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
                onClick: (rowData) => {
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
