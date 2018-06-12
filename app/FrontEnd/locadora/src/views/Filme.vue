<template>
  <div class="filme">
     <div>
        <h3>Filmes</h3>
        <table class="striped">
            <thead>
                <tr>
                    <th width="120px"></th>
                    <th>Nome</th>
                    <th>Data de Criação</th>
                    <th>Gênero</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="filme in this.filmes" :key="filme.Id">
                    <td v-if="editandoFilme === filme.Id">
                        <button v-on:click="atualizarFilme(filme)" class="btn-floating waves-effect waves-light green"><i  class="material-icons"> save </i> </button>
                    </td>
                    <td v-else>                        
                        <button v-on:click="editandoFilme = filme.Id" class="btn-floating waves-effect waves-light blue"><i  class="material-icons"> edit </i> </button>
                        <button v-on:click="excluirFilme(filme)" class="btn-floating waves-effect waves-light red"><i  class="material-icons"> delete </i> </button>
                    </td>
                    <td v-if="editandoFilme === filme.Id">
                       <input type="text" class="validate" v-model="filme.Nome">
                    </td>
                    <td v-else>
                        {{filme.Nome}}
                    </td>
                    <td v-if="editandoFilme === filme.Id">
                        <input type="text" v-model="filme.DateDeCriacao" >
                    </td>
                    <td v-else>
                        {{filme.DateDeCriacao}}
                    </td>
                    <td v-if="editandoFilme === filme.Id">
                        <select type="text" class="browser-default" v-model="filme.GeneroID">
                            <option v-for="genero in generos" :key="genero.Id" :value="genero.Id">{{genero.Nome}}</option>
                        </select>
                    </td>
                    <td v-else>
                        {{getGenero(filme.GeneroID)}}
                    </td>
                </tr>
                <tr>
                    <td>
                        <button v-on:click="criarFilme()" class="btn-floating waves-effect waves-light green"><i  class="material-icons"> add </i> </button>
                    </td>
                    <td>
                        <input type="text" class="validate" v-model="novoFilme.Nome">
                    </td>
                    <td>
                        <input type="text" v-model="novoFilme.DateDeCriacao" >
                    </td>
                    <td>
                        <select type="text" class="browser-default" v-model="novoFilme.GeneroID">
                            <option v-for="genero in this.generos" :key="genero.Id" :value="genero.Id">{{genero.Nome}}</option>
                        </select>
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
        filmes : null,
        generos : null,
        novoFilme: {
            Nome : "",
            DateDeCriacao : "",
            Ativo : true,
            GeneroID : "",
        },
        editandoFilme: null        
      }
  },
  components: {
  },
  methods: {
        atualizarFilme(filme) {
            if(this.editandoFilme == null){
                this.editandoFilme = filme.Id    
            } else {
                fetch(localStorage.link + "/api/Filme", {
                    body: JSON.stringify(filme),
                    method: "PUT",
                    headers: {
                        "Content-Type": "application/json",
                        "Authorization" : "Bearer " + localStorage.token,                    
                    },
                });
                this.editandoFilme = null;
            }
        },
        excluirFilme(filme) {
            fetch(localStorage.link + "/api/Filme", {
                body: JSON.stringify(filme),
                method: "DELETE",
                headers: {
                    "Content-Type": "application/json",
                    "Authorization" : "Bearer " + localStorage.token,                    
                },
            });
            location.reload();
        },
        criarFilme() {
            fetch(localStorage.link + "/api/Filme", {
                body: JSON.stringify(this.novoFilme),
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    "Authorization" : "Bearer " + localStorage.token,                    
                },
            });
            this.editandoFilme = null;
            location.reload();
        },
        getGenero(generoId){
            return this.generos.find(element => element.Id == generoId).Nome
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
            fetch(localStorage.link + "/api/Filme", { 
                    headers : {
                        "Authorization" : "Bearer " + localStorage.token 
                    }
            })
            .then(response => response.json())
            .then((data) => {
                this.filmes = data;
            })
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