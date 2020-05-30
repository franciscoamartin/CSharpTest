import api from './api';

export async function createSupplier(supplier) {
  const response = await api.post(`/supplier`, supplier);
  return response.data;
}
export async function getSupplier(id) {
  const response = await api.get(`/supplier/${id}`);
  return response.data;
}
export async function getSupplierByName(name) {
  const response = await api.get(`/supplier/${name}`);
  return response.data;
}
export async function getSupplierByDocument(document) {
  const response = await api.get(`/supplier/${document}`);
  return response.data;
}
export async function getSupplierByRegisterTime(registerTime) {
  const response = await api.get(`/supplier/${registerTime}`);
  return response.data;
}
export async function getSupplierByCompany(company) {
  const response = await api.get(`/supplier/${company}`);
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
