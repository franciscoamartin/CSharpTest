import React from 'react';
import MaterialTable from 'material-table';
import swal from 'sweetalert';
import * as companyService from '../../services/supplierServices';
import showModal from '../LoadingModal';

import './styles.css';

export default function SuppliersTable({
  suppliers,
  setSuppliers
}) {
  const [columns, setColumns] = React.useState([
    {
      title: 'Nome',
      field: 'name',
    },
    { title: 'CNPJ', field: 'cpfCnpj', editable: 'never' },
  ]);

  async function handleUpdate(newData, oldData) {
    const accepted = await swal(
      'Tem que certeza deseja editar essa empresa?',
      '',
      'info'
    );
    if (accepted) {
      try {
        showModal();
        const dataToSend = {
          id: oldData.id,
          cnpj: oldData.cnpj,
          uf: newData.uf,
          tradingName: newData.tradingName,
        };
        await companyService.updateCompany(dataToSend);
        swal('Empresa alterada com sucesso', '', 'success');
      } catch (error) {
        swal('Empresa não foi alterada', '', 'error');
      }
    }
  }

  async function handleDelete(event, rowData) {
    const accepted = await swal(
      'Tem que certeza deseja deletar essa empresa?',
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

  return (
    <section>
      <MaterialTable
        title="Selecione uma empresa para ver seus fornecedores"
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
    </section>
  );
}
