import React, { useState, useEffect } from 'react';
import { useHistory } from 'react-router-dom';
import swal from 'sweetalert';
import * as companyService from '../../services/companyServices';
import ReactDOM from 'react-dom';
import CompaniesTable from '../../components/CompaniesTable';

import api from '../../services/api';
import './styles.css';

export default function Supplier() {
  const [company, setCompany] = useState('');
  const [companies, setCompanies] = useState([]);
  const [name, setName] = useState('');
  const [cpf, setCPF] = useState('');
  const [cnpj, setCNPJ] = useState('');
  const [telephone, setTelephone] = useState('');
  const history = useHistory();

  useEffect(() => {
    getAllCompanies();
  }, []);

  async function getAllCompanies() {
    const companiesFound = companyService.getAllCompanies();
    setCompanies(companiesFound);
  }

  async function handleRegister(e) {
    e.preventDefault();

    const data = {
      company,
      name,
      cpf,
      cnpj,
      telephone,
    };

    try {
      const response = await api.post('suppliers', data);

      swal(`Fornecedor cadastrado com sucesso!`);
    } catch (error) {
      swal(`Erro ao cadastrar, tente novamente.`);
    }
  }

  function showModal() {
    let wrapper = document.createElement('div');
    ReactDOM.render(
      <CompaniesTable companies={companies}></CompaniesTable>,
      wrapper
    );
    let el = wrapper.firstChild;

    swal({
      title: 'Selecione uma empresa para ver seus fornecedores',
      content: el,
    });
  }

  return (
    <div className="supplier-container">
      <div className="content">
        <section>
          <h1 onClick={showModal}>Cadastro de fornecedores</h1>

          <form onSubmit={handleRegister}>
            <input
              placeholder="Empresa"
              value={company}
              onChange={(e) => setCompany(e.target.value)}
            />
            <input
              placeholder="Nome"
              value={name}
              onChange={(e) => setName(e.target.value)}
            />

            <div className="input-group">
              <input
                placeholder="CPF"
                value={cpf}
                onChange={(e) => setCPF(e.target.value)}
              />
              <input
                placeholder="CNPJ"
                value={cnpj}
                onChange={(e) => setCNPJ(e.target.value)}
              />
            </div>
            <button className="button" type="submit">
              Cadastrar
            </button>
          </form>
        </section>
      </div>
    </div>
  );
}
