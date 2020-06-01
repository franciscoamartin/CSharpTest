import React, { useState, useEffect } from 'react';
import { useHistory } from 'react-router-dom';
import * as companyService from '../../services/companyServices';
import CompaniesTable from '../../components/CompaniesTable';
import ReactDOM from 'react-dom';
import InputMask from 'react-input-mask';
import ReactLoading from 'react-loading';
import swal from 'sweetalert';

import './styles.css';

export default function Company() {
  const [isLoading, setIsLoading] = useState(false);
  const [companies, setCompanies] = useState([]);
  const [tradingName, setTradingName] = useState('');
  const [uf, setUF] = useState('');
  const [cnpj, setCNPJ] = useState('');

  useEffect(() => {
    getAll();
  }, []);

  useEffect(() => {
    getAll();
  }, []);

  async function handleRegister(e) {
    setIsLoading(true);
    e.preventDefault();

    const data = {
      tradingName,
      uf,
      cnpj,
    };

    try {
      const response = await companyService.createCompany(data);
      swal(`Empresa cadastrada com sucesso!`, '', 'success');
      setIsLoading(false);
      getAll();
      clearInputData();
    } catch (error) {
      setIsLoading(false);
      swal(`Erro ao cadastrar, tente novamente.`, '', 'error');
    }
  }

  const dataBaseMethods = {
    getAll: getAll,
    getById: getById,
  };

  async function getAll() {
    const companiesFound = await companyService.getAllCompanies();
    setCompanies(companiesFound);
  }

  async function getById() {
    try {
      const companies = await companyService.getCompany();
    } catch (error) {
      swal(`trocar mensagem aqui!:)`);
    }
  }

  function clearInputData() {
    setTradingName('');
    setCNPJ('');
    setUF('');
  }

  return (
    <div className="company-container">
      <div className="content">
        <section>
          <h1>Cadastro de empresas</h1>

          <form onSubmit={handleRegister}>
            <input
              placeholder="Nome Fantasia"
              value={tradingName}
              onChange={(e) => setTradingName(e.target.value)}
            />

            <div className="input-group">
              <input
                className="input-uf"
                type="text"
                maxlength="2"
                placeholder="UF"
                value={uf}
                onChange={(e) => setUF(e.target.value)}
              />
              <InputMask
                mask="99.999.999/9999-99"
                value={cnpj}
                onChange={(e) => setCNPJ(e.target.value)}
                placeholder="CNPJ"
              ></InputMask>
            </div>

            {isLoading ? (
              <ReactLoading
                type="spinningBubbles"
                color="var(--color-red)"
                height="20px"
                width="20px"
              />
            ) : (
                <button className="button" type="submit">
                  Cadastrar
                </button>
              )}
          </form>
        </section>
        <section>
          <CompaniesTable
            companies={companies}
            setCompanies={setCompanies}
          ></CompaniesTable>
        </section>
      </div>
    </div>
  );
}
