import api from './api'

export const createTransaction = (purchaseForm) => api.post('/Transaction', purchaseForm)

export const getMovements = (clientId) => api.get('/Transaction', { params: { clientId } })
