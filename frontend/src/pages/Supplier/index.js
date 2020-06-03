import React, { useState, useEffect } from 'react';
import { useHistory } from 'react-router-dom';
import * as companyService from '../../services/companyServices';
import * as supplierService from '../../services/supplierServices';
import ReactDOM from 'react-dom';
import CompaniesTable from '../../components/Tables/CompaniesTable/index';
import SuppliersTable from '../../components/Tables/SuppliersTable/index';
import EntityType from '../../components/EntityType/index';
import SearchSupplier from '../../components/SearchSupplier/index';
import validateSupplier from '../../services/validators/supplierValidator';
import swal from 'sweetalert';
import ReactLoading from 'react-loading';

import './styles.css';

export default function Supplier() {
  const [suppliers, setSuppliers] = useState([]);
  const [isLoading, setIsLoading] = useState(false);
  const [companySelected, setCompanySelected] = useState('');
  const [companies, setCompanies] = useState([]);
  const [name, setName] = useState('');
  const [documentType, setDocumentType] = useState(1);
  const [documentNumber, setDocumentNumber] = useState('');
  const [rG, setRG] = useState();
  const [birthDate, setBirthDate] = useState();
  const [telephones, setTelephones] = useState([]);
  const [telephone, setTelephone] = useState('');

  const history = useHistory();

  useEffect(() => {
    getAllCompanies();
    getAll();
  }, []);

  async function getAllCompanies() {
    const companiesFound = await companyService.getAllCompanies();
    setCompanies(companiesFound);
  }

  async function handleRegister(e) {
    setIsLoading(true);
    e.preventDefault();

    const dataToSend = {
      companyId: companySelected.id,
      name,
      document: { number: documentNumber, type: documentType },
      rG,
      birthDate: birthDate && new Date(birthDate),
      telephones: telephones,
    };

    try {
      validateSupplier(dataToSend);
    } catch (error) {
      setIsLoading(false);
      return swal(error.message);
    }

    try {
      const response = await supplierService.createSupplier(dataToSend);
      swal(`Fornecedor cadastrado com sucesso!`, '', 'success');
      setIsLoading(false);
      getAll();
      clearInputData();
    } catch (error) {
      setIsLoading(false);
      swal(
        error.response
          ? error.response.message
          : 'Erro ao cadastrar,tente novamente',
        '',
        'error'
      );
    }
  }

  async function getAll() {
    const suppliersFound = await supplierService.getAllSuppliers();
    setSuppliers(suppliersFound);
  }

  function showModal() {
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

  function addTelephone() {
    const foundTelephone = telephones.find((t) => t.number == telephone);
    if (!foundTelephone) {
      telephones.push({ number: '+55' + ' ' + telephone });
      setTelephones(Object.assign([], telephones));
      setTelephone('');
    }
  }

  function clearInputData() {
    setName('');
    setDocumentNumber('');
    setRG('');
    setBirthDate('');
    setTelephone('');
    setTelephones([]);
    setCompanySelected({});
  }

  return (
    <div className="supplier-container">
      <div className="content">
        <section>
          <div>
            <h1>Cadastro de fornecedores</h1>

            <form onSubmit={handleRegister}>
              <button
                className="company-button"
                onClick={showModal}
                type="button"
              >
                Selecionar empresa
              </button>
              <button
                className="company-button"
                type="button"
                onClick={() => history.push('/company')}
              >
                Cadastrar empresa
              </button>
              {companySelected.tradingName && (
                <div className="selected-company">
                  <p>Nome: {companySelected.tradingName}</p>
                  <p>CNPJ: {companySelected.cnpj}</p>
                  <p>UF: {companySelected.uf}</p>
                </div>
              )}
              <input
                placeholder="Nome"
                value={name}
                onChange={(e) => setName(e.target.value)}
              />

              <div className="person-group">
                <select
                  className="person-group"
                  onChange={(e) => {
                    const stringValue = e.target.value;
                    setDocumentType(Number(stringValue));
                  }}
                >
                  <option value={1}>CNPJ</option>
                  <option value={2}>CPF</option>
                </select>

                <form>
                  <EntityType
                    className="person-group"
                    documentType={documentType}
                    documentNumber={documentNumber}
                    setDocumentNumber={setDocumentNumber}
                    rg={rG}
                    setRG={setRG}
                    birthDate={birthDate}
                    setBirthDate={setBirthDate}
                  ></EntityType>
                </form>
              </div>
              <div className="input-group">
                <input
                  type="number"
                  placeholder="Telefone"
                  value={telephone}
                  onChange={(e) => setTelephone(e.target.value)}
                />
                <img
                  className="telephone-button"
                  src="plus.svg"
                  alt="Adicionar telefone"
                  onClick={addTelephone}
                />
              </div>
              <div className="input-group">
                {telephones.map((tel) => (
                  <div className="telephone-list" key={tel.number}>
                    <p className="telephone-number">{tel.number}</p>
                    <img
                      className="telephone-delete"
                      src="delete.svg"
                      alt="Deletar telefone"
                      onClick={() => {
                        const filteredTelephones = telephones.filter(
                          (t) => t.number != tel.number
                        );
                        setTelephones(filteredTelephones);
                      }}
                    />
                  </div>
                ))}
              </div>
              {isLoading ? (
                <ReactLoading
                  type="spinningBubbles"
                  color="var(--color-red)"
                  height="40px"
                  width="40px"
                />
              ) : (
                <button className="button" type="submit">
                  Cadastrar
                </button>
              )}
            </form>
          </div>
          <div>
            <h1>Busca de fornecedores</h1>
            <div className="input-group">
              <SearchSupplier companies={companies}></SearchSupplier>
            </div>
          </div>
        </section>
        <section>
          <SuppliersTable
            suppliers={suppliers}
            setSuppliers={setSuppliers}
          ></SuppliersTable>
        </section>
      </div>
    </div>
  );
}
