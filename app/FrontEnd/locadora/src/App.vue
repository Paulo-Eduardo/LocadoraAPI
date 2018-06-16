<template>
  <div id="app">
    <div id="nav">
      <router-link to="/locacao">Locação</router-link> |
      <router-link to="/filme">Filmes</router-link> |
      <router-link to="/genero">Gêneros</router-link>
    </div>
    <div class="title">
      <h1> Locadora Maggioni </h1>
    </div>
    <div id="nav">
    </div>
    <router-view/>
  </div>
</template>

<script>
</script>

<style>
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}

#nav a {
  font-weight: bold;
  color: #2c3e50;
}

#nav a.router-link-exact-active {
  color: #42b983;
}

.title {
  width:100%;
  text-align: left;
  margin-left: 30px
}

h1 {
  font-family: Georgia, 'Times New Roman', Times, serif
}
</style>