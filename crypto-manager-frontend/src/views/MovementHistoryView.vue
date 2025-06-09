<script setup>
import { ref, onMounted } from 'vue';
import { fetchClients }   from '../services/clientService';
import { getMovements }   from '../services/transactionService';

const clients   = ref([]);
const clientId  = ref(null);
const movements = ref([]);

onMounted(async () => {
  // Carga la lista de clientes para el select
  const { data } = await fetchClients();
  clients.value  = data;
});

const loadMovements = async () => {
  if (!clientId.value) return;
  // Obtiene los movimientos del cliente seleccionado
  const { data } = await getMovements(clientId.value);
  movements.value = data;
};
</script>

<template>
  <div class="history-view">
    <h2 class="history-title">Movement History</h2>

    <div class="filter-group">
      <select v-model="clientId" class="filter-select">
        <option :value="null" disabled>Select client</option>
        <option v-for="c in clients" :key="c.id" :value="c.id">
          {{ c.name }}
        </option>
      </select>
      <button @click="loadMovements" class="filter-button">
        Search
      </button>
    </div>

    <table v-if="movements.length" class="history-table">
      <thead>
        <tr>
          <th class="history-th">Date</th>
          <th class="history-th">Crypto</th>
          <th class="history-th">Action</th>
          <th class="history-th">Amount</th>
          <th class="history-th">Money (ARS)</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="m in movements" :key="m.id" class="history-row">
          <td class="history-td">{{ new Date(m.dateTime).toLocaleString() }}</td>
          <td class="history-td">{{ m.cryptoCode }}</td>
          <td class="history-td">{{ m.action }}</td>
          <td class="history-td">{{ m.cryptoAmount }}</td>
          <td class="history-td">
            {{ m.money.toLocaleString('es-AR', { style: 'currency', currency: 'ARS' }) }}
          </td>
        </tr>
      </tbody>
    </table>

    <p v-else class="no-data">No movements to display.</p>
  </div>
</template>

<style scoped>
.history-view {
  max-width: 700px;
  margin: 2rem auto;
  padding: 1.5rem;
  border: 1px solid #ccc;
  border-radius: 8px;
  background: #fafafa;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}

.history-title {
  margin-bottom: 1rem;
  font-size: 1.5rem;
  text-align: center;
  color: #333;
}

.filter-group {
  display: flex;
  gap: 0.5rem;
  margin-bottom: 1rem;
  justify-content: center;
}

.filter-select {
  flex: 1;
  padding: 0.5rem;
  border: 1px solid #bbb;
  border-radius: 4px;
  font-size: 1rem;
}

.filter-button {
  padding: 0.5rem 1rem;
  background-color: #007bff;
  border: none;
  border-radius: 4px;
  color: white;
  font-size: 1rem;
  font-weight: 600;
  cursor: pointer;
  transition: background-color 0.2s ease;
}

.filter-button:hover {
  background-color: #0056b3;
}

.history-table {
  width: 100%;
  border-collapse: collapse;
}

.history-th,
.history-td {
  padding: 0.75rem;
  border: 1px solid #ddd;
  text-align: left;
}

.history-th {
  background-color: #f0f0f0;
  font-weight: 600;
  color: #555;
}

.history-row:nth-child(even) {
  background-color: #fcfcfc;
}

.no-data {
  text-align: center;
  color: #666;
  font-style: italic;
  margin-top: 1rem;
}
</style>
