<template>
  <div class="filme">
     <div>
        <h3>Gênero</h3>
        <table class="striped">
            <thead>
                <tr>
                    <th width="120px"></th>
                    <th>Nome</th>
                    <th>Data de Criação</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="genero in this.generos" :key="genero.Id">
                    <td v-if="editandoGenero === genero.Id">
                        <button v-on:click="atualizarGenero(genero)" class="btn-floating waves-effect waves-light green"><i  class="material-icons"> save </i> </button>
                    </td>
                    <td v-else>                        
                        <button v-on:click="editandoGenero = genero.Id" class="btn-floating waves-effect waves-light blue"><i  class="material-icons"> edit </i> </button>
                        <button v-on:click="excluirGenero(genero)" class="btn-floating waves-effect waves-light red"><i  class="material-icons"> delete </i> </button>
                    </td>
                    <td v-if="editandoGenero === genero.Id">
                       <input type="text" class="validate" v-model="genero.Nome">
                    </td>
                    <td v-else>
                        {{genero.Nome}}
                    </td>
                    <td v-if="editandoGenero === genero.Id">
                        <input type="text" v-model="genero.DataDeCriacao" >
                    </td>
                    <td v-else>
                        {{genero.DataDeCriacao}}
                    </td>
                </tr>
                <tr>
                    <td>
                        <button v-on:click="criarGenero()" class="btn-floating waves-effect waves-light green"><i  class="material-icons"> add </i> </button>
                    </td>
                    <td>
                        <input type="text" class="validate" v-model="novoGenero.Nome">
                    </td>
                    <td>
                        <input type="text" v-model="novoGenero.DataDeCriacao">
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
  </div>
</template>

<script>

export default {
  name: 'home',  
  data() {
      return {
        generos : null,
        editandoGenero: null,
        novoGenero: {
            Nome: "",
            DataDeCriacao: "",
            Ativo: true,
        }   
      }
  },
  methods: {
        atualizarGenero(genero) {
            fetch(localStorage.link + "/api/Genero", {
                body: JSON.stringify(genero),
                method: "PUT",
                headers: {
                    "Content-Type": "application/json",
                    "Authorization" : "Bearer " + localStorage.token,                    
                },
            });
            this.editandoGenero = null;
        },
        excluirGenero(genero) {
            fetch(localStorage.link + "/api/Genero", {
                body: JSON.stringify(genero),
                method: "DELETE",
                headers: {
                    "Content-Type": "application/json",
                    "Authorization" : "Bearer " + localStorage.token,                    
                },
            });
            location.reload();
        },
        criarGenero() {
            fetch(localStorage.link + "/api/Genero", {
                body: JSON.stringify(this.novoGenero),
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    "Authorization" : "Bearer " + localStorage.token,                    
                },
            });
            location.reload();
        },
    },
    mounted() {
        fetch(localStorage.link + "/api/Genero", { 
                headers : {
                    "Authorization" : "Bearer " + localStorage.token 
                }
        })
        .then(response => response.json())
        .then((data) => {
            this.generos = data;
        })
    }
}
</script>

<style>
    @import "https://cdnjs.cloudflare.com/ajax/libs/materialize/1.0.0-beta/css/materialize.min.css";
    @import "https://cdnjs.cloudflare.com/ajax/libs/materialize/1.0.0-beta/js/materialize.min.js";
    @import "https://fonts.googleapis.com/icon?family=Material+Icons";

    button {
        margin-right: 10px;
    }

</style>