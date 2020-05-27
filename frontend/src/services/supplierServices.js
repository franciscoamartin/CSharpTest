import api from './api';

export async function getSupplier(id) {
  const response = await api.get(`/Supplier/${id}`);
  return response.data;
}
export async function getAllSuppliers() {
  debugger;
  const response = await api.get(`/supplier`);
  debugger;
  return response.data;
}
