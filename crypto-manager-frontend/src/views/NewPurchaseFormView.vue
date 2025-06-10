<script setup>
import { ref, onMounted } from 'vue';
import { fetchClients }      from '../services/clientService';
import { createTransaction } from '../services/transactionService';

const clients = ref([]);
const form    = ref({
  cryptoCode: '',
  action: 'purchase',
  cryptoAmount: 0,
  dateTime: '',
  clientId: null
});
const message = ref('');

onMounted(async () => {
  // Carga la lista de clientes para el select
  const { data } = await fetchClients();
  clients.value  = data;
});

const submitForm = async () => {
  try {
    await createTransaction({
      cryptoCode:   form.value.cryptoCode,
      action:       form.value.action,
      cryptoAmount: form.value.cryptoAmount,
      dateTime:     form.value.dateTime,
      clientId:     form.value.clientId
    });
    message.value = 'Purchase saved!';
    // Resetear formulario
    form.value = {
      cryptoCode:   '',
      action:       'purchase',
      cryptoAmount: 0,
      dateTime:     '',
      clientId:     null
    };
  } catch (err) {
    message.value = err.response?.data?.title || 'Oops! Something went wrong.';
  }
};
</script>

<template>
  <div class="purchase-form">
    <h2 class="form-title">New Purchase</h2>

    <div class="form-group">
      <label class="form-label">Client</label>
      <select v-model="form.clientId" class="form-select">
        <option :value="null" disabled>Select client</option>
        <option v-for="c in clients" :key="c.id" :value="c.id">
          {{ c.name }}
        </option>
      </select>
    </div>

    <div class="form-group">
      <label class="form-label">Crypto</label>
      <select v-model="form.cryptoCode" class="form-select">
        <option disabled value="">Choose crypto</option>
        <option value="BTC">Bitcoin</option>
        <option value="ETH">Ethereum</option>
        <option value="USDC">USDC</option>
        <option value="SOL">Solana</option>
        <option value="BNB">BNB</option>
      </select>
    </div>

    <div class="form-group">
      <label class="form-label">Amount</label>
      <input
        v-model.number="form.cryptoAmount"
        type="number"
        step="0.00000001"
        class="form-input"
      />
    </div>

    <div class="form-group">
      <label class="form-label">Date & Time</label>
      <input
        v-model="form.dateTime"
        type="datetime-local"
        class="form-input"
      />
    </div>

    <button @click="submitForm" class="btn-primary">
      Save
    </button>

    <p v-if="message" class="form-message">{{ message }}</p>
  </div>
</template>

<style scoped>
.purchase-form {
  max-width: 400px;
  margin: 2rem auto;
  padding: 1.5rem;
  border: 1px solid #ccc;
  border-radius: 8px;
  background: #fafafa;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}

.form-title {
  margin-bottom: 1rem;
  font-size: 1.5rem;
  text-align: center;
  color: #333;
}

.form-group {
  margin-bottom: 1rem;
}

.form-label {
  display: block;
  margin-bottom: 0.5rem;
  font-weight: 600;
  color: #555;
}

.form-select,
.form-input {
  box-sizing: border-box;
  width: 100%;
  padding: 0.5rem 0.75rem;
  border: 1px solid #bbb;
  border-radius: 4px;
  font-size: 1rem;
  color: #333;
}

.form-select:focus,
.form-input:focus {
  outline: none;
  border-color: #888;
  box-shadow: 0 0 0 2px rgba(136, 136, 136, 0.2);
}

.btn-primary {
  width: 100%;
  padding: 0.75rem;
  background-color: #007bff;
  border: none;
  border-radius: 4px;
  color: white;
  font-size: 1rem;
  font-weight: 600;
  cursor: pointer;
  transition: background-color 0.2s ease;
}

.btn-primary:hover {
  background-color: #0056b3;
}

.form-message {
  margin-top: 1rem;
  text-align: center;
  color: #006400;
  font-weight: 500;
}
</style>
