<script setup>
import { ref, onMounted } from 'vue';
import { fetchClients }   from '../services/clientService';
import { getMovements }   from '../services/transactionService';

const clients   = ref([]);
const clientId  = ref(null);
const movements = ref([]);

onMounted(async () => {
  const { data } = await fetchClients();
  clients.value = data;
});

const loadMovements = async () => {
  const { data } = await getMovements(clientId.value);
  movements.value = data;
};
</script>

<template>
  <h2 class="text-xl font-bold mb-4">Movement History</h2>

  <div class="flex gap-2 mb-4">
    <select v-model="clientId" class="border p-1">
      <option :value="null" disabled>Select client</option>
      <option v-for="c in clients" :key="c.id" :value="c.id">{{ c.name }}</option>
    </select>
    <button @click="loadMovements" class="bg-blue-600 text-white px-3 py-1 rounded">Search</button>
  </div>

  <table v-if="movements.length" class="w-full border">
    <thead>
      <tr class="bg-gray-100">
        <th>Date</th>
        <th>Crypto</th>
        <th>Action</th>
        <th>Amount</th>
        <th>Money (ARS)</th>
      </tr>
    </thead>
    <tbody>
      <tr v-for="m in movements" :key="m.id" class="border-b">
        <td>{{ new Date(m.dateTime).toLocaleString() }}</td>
        <td>{{ m.cryptoCode }}</td>
        <td>{{ m.action }}</td>
        <td>{{ m.cryptoAmount }}</td>
        <td>{{ m.money.toLocaleString('es-AR', { style: 'currency', currency: 'ARS' }) }}</td>
      </tr>
    </tbody>
  </table>
</template>
