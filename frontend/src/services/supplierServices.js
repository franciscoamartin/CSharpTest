import api from './api';

export async function createSupplier(supplier) {
  const response = await api.post(`/supplier`, supplier);
  return response.data;
}
export async function getSupplier(id) {
  const response = await api.get(`/supplier/${id}`);
  return response.data;
}
export async function getSupplierByName(name, companyId) {
  let response;
  if (companyId) {
    response = await api.get(`/supplier/name/${name}/${companyId}`);
  } else {
    response = await api.get(`/supplier/name/${name}`);
  }
  return response.data;
}
export async function getSupplierByDocument(document, companyId) {
  let response;
  if (companyId) {
    response = await api.get(`/supplier/document/${document}/${companyId}`);
  } else {
    response = await api.get(`/supplier/document/${document}`);
  }
  return response.data;
}
export async function getSupplierByRegisterTime(registerTime, companyId) {
  let response;
  if (companyId) {
    response = await api.get(
      `/supplier/registerTime/${registerTime}/${companyId}`
    );
  } else {
    response = await api.get(`/supplier/registerTime/${registerTime}`);
  }
  return response.data;
}
export async function getSupplierByCompany(companyId) {
  const response = await api.get(`/supplier/company/${companyId}`);
  return response.data;
}
export async function getAllSuppliers() {
  const response = await api.get(`/supplier`);
  return response.data;
}
export async function updateSupplier(supplier) {
  const response = await api.put(`/supplier/${supplier.id}`, supplier);
  return response.data;
}
export async function deleteSupplier(id) {
  const response = await api.delete(`/supplier/${id}`);
  return response.data;
}
