import React, { useState } from 'react';
import { useHistory } from 'react-router-dom';
import * as companyService from '../../services/companyServices';
import CompaniesTable from '../../components/CompaniesTable';
import ReactDOM from 'react-dom';

import swal from 'sweetalert';

import api from '../../services/api';
import './styles.css';

export default function Company() {
  const [companies, setCompanies] = useState([]);
  const [tradingName, setTradingName] = useState('');
  const [uf, setUF] = useState('');
  const [cnpj, setCNPJ] = useState('');
  const history = useHistory();

  async function handleRegister(e) {
    e.preventDefault();

    const data = {
      tradingName,
      uf,
      cnpj,
    };

    try {
      const response = await api.post('companies', data);

      swal(`Empresa cadastrada com sucesso!`);
    } catch (error) {
      swal(`Erro ao cadastrar, tente novamente.`);
    }
  }

  const dataBaseMethods = {
    getAll: getAll,
    getById: getById,
  };

  async function getAll() {
    debugger;
    const companiesFound = await companyService.getAllCompanies();
    debugger;
    setCompanies(companiesFound);

    debugger;
  }
  async function getById() {
    try {
      const companies = await companyService.getCompany();
    } catch (error) {
      swal(`trocar mensagem aqui!:)`);
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
    <div className="company-container">
      <div className="content">
        <section>
          <h1 onClick={showModal}>Cadastro de empresas</h1>

          <form onSubmit={handleRegister}>
            <input
              placeholder="Nome Fantasia"
              value={tradingName}
              onChange={(e) => setTradingName(e.target.value)}
            />

            <div className="input-group">
              <input
                placeholder="UF"
                value={uf}
                onChange={(e) => setUF(e.target.value)}
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
