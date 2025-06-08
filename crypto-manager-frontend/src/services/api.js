import axios from 'axios'

// Configura la base URL de tu API .NET Core
export default axios.create({
  baseURL: 'https://localhost:7066/api',
  headers: {
    'Content-Type': 'application/json',
  },
})
