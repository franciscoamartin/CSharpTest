import React from 'react';
import MaterialTable from 'material-table';
import swal from 'sweetalert';
import * as supplierService from '../../../services/supplierServices';
import ReactDOM from 'react-dom';
import './styles.css';
import showModalError from '../../../services/showModalError';

export default function SuppliersTable({ suppliers, setSuppliers }) {
  const [columns, setColumns] = React.useState([
    { title: 'Empresa', field: 'companyTradingName', editable: 'never' },
    {
      title: 'Nome',
      field: 'name',
    },
    {
      title: 'Telefone',
      field: 'telephones',
      render: (rowData) => (
        <div>
          {rowData.telephones.map((tel) => (
            <p>{tel}</p>
          ))}
        </div>
      ),
    },
    { title: 'Documento', field: 'cpfCnpj', editable: 'never' },
    { title: 'RG', field: 'rg', editable: 'never' },
    { title: 'Data de nascimento', field: 'birthDate', editable: 'never' },
    {
      title: 'Data de cadastro',
      field: 'registerTime',
      editable: 'never',
    },
  ]);

  async function handleUpdate(newData, oldData) {
    try {
      const dataToSend = {
        id: oldData.id,
        name: newData.name,
        telephones: formattedTelephones(newData.telephones),
      };
      await supplierService.updateSupplier(dataToSend);
      swal('Fornecedor alterado com sucesso', '', 'success');
      getAll();
    } catch (error) {
      showModalError(error, 'Fornecedor não foi alterado');
    }
  }

  async function handleDelete(event, rowData) {
    const accepted = await swal(
      'Tem certeza que deseja deletar esse fornecedor?',
      '',
      'info',
      { buttons: ['Cancelar', 'Confirmar'], dangerMode: true }
    );
    if (accepted) {
      try {
        await supplierService.deleteSupplier(rowData.id);
        const filteredSuppliers = suppliers.filter((s) => s.id != rowData.id);
        setSuppliers(filteredSuppliers);
        swal('Fornecedor deletado com sucesso', '', 'success');
      } catch (error) {
        showModalError(error, 'Fornecedor não foi deletado');
      }
    }
  }

  async function getAll() {
    try {
      const suppliersFound = await supplierService.getAllSuppliers();
      setSuppliers(suppliersFound);
    } catch (error) {
      showModalError(error, 'Não foi possível realizar a busca');
    }
  }

  function formattedTelephones(telephones) {
    return telephones.map((tel) => ({
      number: tel,
    }));
  }

  return (
    <div className="suppliers-table-container">
      <MaterialTable
        title="Fornecedores"
        options={{
          search: false,
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
        ]}
      />
    </div>
  );
}
