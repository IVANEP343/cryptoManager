<script setup>
import { RouterLink, RouterView } from 'vue-router'

</script>

<template>
  <header>
    <h1 class="mainHeader">CRYPTO MANAGER ASSETS</h1>

    <div class="navContainer">
      <nav>
        <button><RouterLink to="/new-purchase">new-purchase</RouterLink></button>
        <button><RouterLink to="/History">MovementHistory</RouterLink></button>
      </nav>
    </div>
  </header>

  <RouterView />
</template>

<style scoped>
button {
  background-color: #4CAF50; /* Green */
  border: none;
  padding: 15px 32px;
  text-align: center;
  display: inline-block;
  font-size: 16px;
  margin: 4px 2px;
  cursor: pointer;
  text-decoration: none;
}
button:hover {
  background-color: #45a049; /* Darker green */
}
.navContainer {
  background-color: rgb(37, 54, 54);
  padding: 10px;
  border-radius: 5px;
  margin-bottom: 20px;
  display: flex;
  justify-content: center;
  margin-bottom: 20px;
}
.mainHeader {
  text-align: center;
  color: black;
  font-size: 2em;
  margin-bottom: 20px;
}

</style>
