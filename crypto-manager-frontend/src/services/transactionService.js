import api from './api'

export const createTransaction = (payload) => api.post('/Transaction', payload)

export const getMovements = (clientId) => api.get('/Transaction', { params: { clientId } })
