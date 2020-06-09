import React, { useState } from 'react';
import ReactDOM from 'react-dom';
import swal from 'sweetalert';
import CompaniesTable from '../../components/Tables/CompaniesTable/index';
import * as supplierService from '../../services/supplierServices';
import ReactLoading from 'react-loading';
import InputMask from 'react-input-mask';
import showModalError from '../../services/showModalError';

import './styles.css';

export default function SearchSupplier({
  companies,
  setSuppliers,
  isCompanyLoading,
  getAll,
}) {
  const [isLoading, setIsLoading] = useState(false);
  const [searchOption, setSearchOption] = useState('Nome');
  const [searchString, setSearchString] = useState('');
  const [companySelected, setCompanySelected] = useState({});
  const [showClearSearchBtn, setShowClearSearchBtn] = useState(false);

  function showCompaniesModal() {
    let wrapper = document.createElement('div');
    ReactDOM.render(
      <CompaniesTable
        companies={companies}
        setCompanySelected={setCompanySelected}
      ></CompaniesTable>,
      wrapper
    );
    let el = wrapper.firstChild;

    swal({
      title: 'Selecione uma empresa para ver seus fornecedores',
      content: el,
    });
  }

  async function handleSearch(e) {
    e.preventDefault();
    try {
      if (searchString.trim().length == 0) {
        return swal('Insira um dado para realizar a busca', '', 'error');
      }
      setIsLoading(true);
      const suppliersFound = await searchBy[searchOption]();
      setSuppliers(suppliersFound);
      setIsLoading(false);
      if (!showClearSearchBtn) setShowClearSearchBtn(true);
    } catch (error) {
      setIsLoading(false);
      showModalError(error, 'Erro ao realizar a busca');
    }
  }

  const searchBy = {
    Nome: async () =>
      supplierService.getSupplierByName(searchString, companySelected.id),
    CPF: async () =>
      supplierService.getSupplierByDocument(searchString, companySelected.id),
    CNPJ: async () =>
      supplierService.getSupplierByDocument(searchString, companySelected.id),
    DataDeCadastro: async () =>
      supplierService.getSupplierByRegisterTime(
        searchString,
        companySelected.id
      ),
  };

  return (
    <form onSubmit={handleSearch}>
      <div className="search-supplier">
        <div className="select-company">
          <select
            className="search"
            onChange={(e) => {
              setSearchOption(e.target.value);
            }}
          >
            <option value={'Nome'}>Nome</option>
            <option value={'CPF'}>CPF</option>
            <option value={'CNPJ'}>CNPJ</option>
            <option value={'DataDeCadastro'}>Data de cadastro</option>
          </select>

          {isCompanyLoading ? (
            <div className="loading" style={{ width: '70%' }}>
              <ReactLoading
                type="spinningBubbles"
                color="var(--color-red)"
                height="40px"
                width="40px"
              />
            </div>
          ) : (
            <button
              className="company-button"
              onClick={showCompaniesModal}
              type="button"
            >
              Selecionar empresa
            </button>
          )}
        </div>
        {companySelected.tradingName && (
          <div className="selected-company-container">
            <div className="selected-company">
              <p>Empresa: {companySelected.tradingName}</p>
              <p>CNPJ: {companySelected.cnpj}</p>
              <p>UF: {companySelected.uf}</p>
            </div>
            <img
              className="remove-company"
              src="cancel.svg"
              alt="Remover empresa"
              onClick={() => setCompanySelected({})}
            />
          </div>
        )}

        {searchOption == 'CPF' || searchOption == 'CNPJ' ? (
          <InputMask
            mask={
              searchOption == 'CPF' ? '999.999.999-99' : '99.999.999/9999-99'
            }
            value={searchString}
            onChange={(e) => setSearchString(e.target.value)}
          >
            {(inputProps) => (
              <input {...inputProps} placeholder={searchOption} />
            )}
          </InputMask>
        ) : (
          <input
            placeholder={searchOption}
            type={searchOption == 'DataDeCadastro' ? 'date' : 'text'}
            value={searchString}
            onChange={(e) => setSearchString(e.target.value)}
          />
        )}
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
          <>
            <button className="button" type="submit">
              Pesquisar
            </button>
            {showClearSearchBtn && (
              <button className="button" type="button" onClick={getAll}>
                Limpar busca
              </button>
            )}
          </>
        )}
      </div>
    </form>
  );
}
