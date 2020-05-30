import api from './api';

export async function createCompany(company) {
  const response = await api.post(`/company`, company);
  return response.data;
}
export async function getCompany(id) {
  const response = await api.get(`/company/${id}`);
  return response.data;
}
export async function getAllCompanies() {
  const response = await api.get(`/company`);
  return response.data;
}
export async function updateCompany(company) {
  const response = await api.put(`/company/${company.id}`, company);
  return response.data;
}
export async function deleteCompany(id) {
  const response = await api.delete(`/company/${id}`);
  return response.data;
}
