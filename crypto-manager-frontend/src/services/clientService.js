import api from './api'

export const fetchClients = () => api.get('/Client')
