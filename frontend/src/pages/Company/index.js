import React, { useState, useEffect } from 'react';
import { useHistory } from 'react-router-dom';
import * as companyService from '../../services/companyServices';
import CompaniesTable from '../../components/Tables/CompaniesTable/index';
import ReactDOM from 'react-dom';
import InputMask from 'react-input-mask';
import ReactLoading from 'react-loading';
import SelectBrasilState from '../../components/SelectBrasilState';
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

  async function getAll() {
    const companiesFound = await companyService.getAllCompanies();
    setCompanies(companiesFound);
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
          <div>
            <h1>Cadastro de empresas</h1>

            <form onSubmit={handleRegister}>
              <input
                placeholder="Nome Fantasia"
                value={tradingName}
                onChange={(e) => setTradingName(e.target.value)}
              />

              <div className="input-group">
                <SelectBrasilState setUF={setUF}></SelectBrasilState>
                <InputMask
                  mask="99.999.999/9999-99"
                  value={cnpj}
                  onChange={(e) => setCNPJ(e.target.value)}
                  placeholder="CNPJ"
                ></InputMask>
              </div>

              {isLoading ? (
                <div className="loading">
                  <ReactLoading
                    type="spinningBubbles"
                    color="var(--color-red)"
                    height="40px"
                    width="40px"
                  />
                </div>
              ) : (
                <button className="button" type="submit">
                  Cadastrar
                </button>
              )}
            </form>
          </div>
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
