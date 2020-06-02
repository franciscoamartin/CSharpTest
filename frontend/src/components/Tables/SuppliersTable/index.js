import React from 'react';
import MaterialTable from 'material-table';
import swal from 'sweetalert';
import * as supplierService from '../../../services/supplierServices';
import showModal from '../../LoadingModal';
import './styles.css';

export default function SuppliersTable({ suppliers, setSuppliers }) {
  const [columns, setColumns] = React.useState([
    { title: 'Empresa', field: 'companyTradingName', editable: 'never' },
    {
      title: 'Nome',
      field: 'name',
    },
    { title: 'Documento', field: 'cpfCnpj', editable: 'never' },
    { title: 'RG', field: 'rg', editable: 'never' },
    { title: 'Data de nascimento', field: 'birthDate', editable: 'never' },
    { title: 'Telefone', field: 'telephone' },
    { title: 'Data de cadastro', field: 'registerTime', editable: 'never' },
  ]);

  async function handleUpdate(newData, oldData) {
    // const accepted = await swal(
    //   'Tem que certeza deseja editar essa empresa?',
    //   '',
    //   'info'
    // );
    // if (accepted) {
    //   try {
    //     showModal();
    //     const dataToSend = {
    //       id: oldData.id,
    //       cnpj: oldData.cnpj,
    //       uf: newData.uf,
    //       tradingName: newData.tradingName,
    //     };
    //     await companyService.updateCompany(dataToSend);
    //     swal('Empresa alterada com sucesso', '', 'success');
    //   } catch (error) {
    //     swal('Empresa não foi alterada', '', 'error');
    //   }
    // }
  }

  async function handleDelete(event, rowData) {
    // const accepted = await swal(
    //   'Tem que certeza deseja deletar essa empresa?',
    //   '',
    //   'info'
    // );
    // if (accepted) {
    //   try {
    //     showModal();
    //     await companyService.deleteCompany(rowData.id);
    //     const filteredCompanies = companies.filter((c) => c.id != rowData.id);
    //     setCompanies(filteredCompanies);
    //     swal('Empresa deletada com sucesso', '', 'success');
    //   } catch (error) {
    //     swal('Empresa não foi deletada', '', 'error');
    //   }
    // }
  }

  function showTelephones(event, rowData) {
    showModal();
  }

  return (
    <div className="suppliers-table-container">
      <MaterialTable
        title="Fornecedores"
        options={{
          filtering: false,
        }}
        columns={columns}
        data={suppliers}
        editable={{
          onRowUpdate: handleUpdate,
        }}
        actions={[
          {
            icon: 'delete',
            tooltip: 'Deletar',
            onClick: handleDelete,
          },
          {
            icon: 'call',
            tooltip: 'Ver telefones',
            onClick: showTelephones,
          },
        ]}
      />
    </div>
  );
}
