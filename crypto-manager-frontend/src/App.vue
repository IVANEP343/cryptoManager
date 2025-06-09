<script setup>
import { RouterLink, RouterView } from 'vue-router';


</script>

<template>
  <div class="app-container" >
    <header class="app-header">
      <h1 class="app-title">Crypto Assets Manager</h1>
      <nav class="app-nav">
        <RouterLink to="/new-purchase" class="nav-button" active-class="nav-button--active">
          New Purchase
        </RouterLink>
        <RouterLink to="/history" class="nav-button" active-class="nav-button--active">
          Movement History
        </RouterLink>
      </nav>
    </header>

    <main class="app-main">
      <RouterView />
    </main>
  </div>
</template>

<style scoped>
.app-container {
  display: flex;
  flex-direction: column;
  min-height: 100vh;
  background-color: grey;

}

.app-header {
  background-color: rgba(0, 123, 255, 0.8);
  /* background-color: #007bff; */
  padding: 1rem;
  color: white;
  text-align: center;
}

.app-title {
  margin: 0;
  font-size: 2rem;
}

.app-nav {
  margin-top: 0.75rem;
  display: flex;
  justify-content: center;
  gap: 1rem;
}

.nav-button {
  display: inline-block;
  padding: 0.5rem 1rem;
  background-color: #0056b3;
  color: white;
  text-decoration: none;
  border-radius: 4px;
  font-weight: 600;
  transition: background-color 0.2s;
}

.nav-button:hover {
  background-color: #004494;
}

.nav-button--active {
  background-color: #004080;
}

.app-main {
  flex: 1;
  padding: 2rem 1rem;
  /* background: #f5f5f5; */
  background: transparent;
}
</style>
