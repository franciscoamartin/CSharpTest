import React, { useState, useEffect } from 'react';
import { useHistory } from 'react-router-dom';
import * as companyService from '../../services/companyServices';
import * as supplierService from '../../services/supplierServices';
import ReactDOM from 'react-dom';
import CompaniesTable from '../../components/Tables/CompaniesTable/index';
import SuppliersTable from '../../components/Tables/SuppliersTable/index';
import EntityType from '../../components/EntityType/index';
import SearchSupplier from '../../components/SearchSupplier/index';
import validateSupplier, {
  isTelephoneValid,
} from '../../services/validators/supplierValidator';
import swal from 'sweetalert';
import ReactLoading from 'react-loading';
import showModalError from '../../services/showModalError';

import './styles.css';

export default function Supplier() {
  const [suppliers, setSuppliers] = useState([]);
  const [isLoading, setIsLoading] = useState(false);
  const [isCompanyLoading, setIsCompanyLoading] = useState(true);
  const [companySelected, setCompanySelected] = useState('');
  const [companies, setCompanies] = useState([]);
  const [name, setName] = useState('');
  const [documentType, setDocumentType] = useState(1);
  const [documentNumber, setDocumentNumber] = useState('');
  const [rG, setRG] = useState();
  const [birthDate, setBirthDate] = useState();
  const [ddi, setDdi] = useState('+55');
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
    setIsCompanyLoading(false);
  }

  async function handleRegister(e) {
    setIsLoading(true);
    e.preventDefault();

    const dataToSend = {
      companyId: companySelected.id,
      name,
      document: { number: documentNumber, type: documentType },
      rG,
      birthDate: getFormattedDate(birthDate),
      telephones: telephones,
    };

    try {
      validateSupplier(dataToSend);
    } catch (error) {
      setIsLoading(false);
      return swal(error.message, '', 'error');
    }

    try {
      await supplierService.createSupplier(dataToSend);
      swal(`Fornecedor cadastrado com sucesso!`, '', 'success');
      setIsLoading(false);
      getAll();
      clearInputData();
    } catch (error) {
      setIsLoading(false);
      showModalError(error, 'Erro ao cadastrar,tente novamente');
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
      title: 'Selecione uma empresa para se cadastrar como fornecedor',
      content: el,
    });
  }

  function addTelephone() {
    if (!isTelephoneValid(ddi + ' ' + telephone))
      return swal('Telefone inválido.', '', 'error');
    const foundTelephone = telephones.find((t) => t.number === telephone);
    if (!foundTelephone) {
      telephones.push({ number: ddi + ' ' + telephone });
      setTelephones(Object.assign([], telephones));
      setTelephone('');
    }
  }

  function getFormattedDate(birthDate) {
    if (birthDate === '') return undefined;
    if (birthDate) return new Date(birthDate);
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
              <div className="company-actions">
                {isCompanyLoading ? (
                  <div className="loading" style={{ width: '50%' }}>
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
                      onClick={showModal}
                      type="button"
                    >
                      Selecionar empresa
                    </button>
                  )}
                <button
                  className="company-button"
                  type="button"
                  onClick={() => history.push('/company')}
                >
                  Cadastrar empresa
                </button>
              </div>
              {companySelected.tradingName && (
                <div className="selected-company">
                  <p>Empresa: {companySelected.tradingName}</p>
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
                <EntityType
                  documentType={documentType}
                  setDocumentType={setDocumentType}
                  documentNumber={documentNumber}
                  setDocumentNumber={setDocumentNumber}
                  rg={rG}
                  setRG={setRG}
                  birthDate={birthDate}
                  setBirthDate={setBirthDate}
                ></EntityType>
              </div>
              <div className="input-group">
                <input
                  className="input-ddi"
                  type="text"
                  placeholder="DDI"
                  value={ddi}
                  onChange={(e) => setDdi(e.target.value)}
                />
                <input
                  type="text"
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
              <div className="input-group column">
                {telephones.map((tel) => (
                  <div className="telephone-list" key={tel.number}>
                    <p className="telephone-number">{tel.number}</p>
                    <img
                      className="telephone-delete"
                      src="delete.svg"
                      alt="Deletar telefone"
                      onClick={() => {
                        const filteredTelephones = telephones.filter(
                          (t) => t.number !== tel.number
                        );
                        setTelephones(filteredTelephones);
                      }}
                    />
                  </div>
                ))}
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
          <div>
            <h1>Busca de fornecedores</h1>
            <div className="input-group">
              <SearchSupplier
                isCompanyLoading={isCompanyLoading}
                companies={companies}
                setSuppliers={setSuppliers}
                getAll={getAll}
              ></SearchSupplier>
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
