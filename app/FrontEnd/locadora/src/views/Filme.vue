<template>
  <div class="filme">
     <div>
        <h3>Filmes</h3>
        <span v-if="erro"  class="helper-text">{{erro}}</span>
        <table class="striped">
            <thead>
                <tr>
                    <th width="120px"></th>
                    <th width="15%">
                        <label>
                            <input @change="mudarSelecao(selecionarTodos)" type="checkbox" class="filled-in" v-model="selecionarTodos" />
                            <span>Selecionar todos</span>
                        </label>
                    </th>
                    <th>Nome</th>
                    <th>Data de Criação</th>
                    <th>Gênero</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="filme in this.filmes" :key="filme.id">
                    <td v-if="editandoFilme === filme.id">
                        <button v-on:click="atualizarFilme(filme)" class="btn-floating waves-effect waves-light green"><i  class="material-icons"> save </i> </button>
                    </td>
                    <td v-else>                        
                        <button v-on:click="editandoFilme = filme.id" class="btn-floating waves-effect waves-light blue"><i  class="material-icons"> edit </i> </button>
                        <button v-on:click="excluirFilme(filme)" class="btn-floating waves-effect waves-light red"><i  class="material-icons"> delete </i> </button>
                    </td>
                    <td>
                        <label>
                            <input type="checkbox" class="filled-in" v-model="filme.Remover"/>
                            <span></span>
                        </label>
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
                        <select type="text" class="browser-default" v-model="filme.generoID">
                            <option v-for="genero in generos" :key="genero.id" :value="genero.Id">{{genero.Nome}}</option>
                        </select>
                    </td>
                    <td v-else>
                        {{getGenero(filme.generoID)}}
                    </td>
                </tr>
                <tr>
                    <a v-on:click="removerSelecionados()" class="waves-effect waves-light btn"> Remover Selecionados </a>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <button v-on:click="criarFilme()" class="btn-floating waves-effect waves-light green"><i  class="material-icons"> add </i> </button>
                    </td>
                    <td>
                        <input type="text" class="validate" v-model="novoFilme.Nome">
                        <span v-if="erroCriarNome"  class="helper-text">{{erroCriarNome}}</span>
                    </td>
                    <td>
                        <input type="text" v-model="novoFilme.DateDeCriacao" >
                        <span v-if="erroCriarData"  class="helper-text">{{erroCriarData}}</span>
                    </td>
                    <td>
                        <select type="text" class="browser-default" v-model="novoFilme.GeneroID">
                            <option v-for="genero in this.generos" :key="genero.id" :value="genero.Id">{{genero.Nome}}</option>
                        </select>
                        <span v-if="erroCriarGenero"  class="helper-text">{{erroCriarGenero}}</span>
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
        selecionarTodos: false,  
        filmes : null,
        generos : null,
        novoFilme: {
            Nome : "",
            DateDeCriacao : "",
            Ativo : true,
            GeneroID : "",
        },
        editandoFilme: null,
        erroCriarData: null,
        erroCriarNome: null,
        erroCriarGenero: null,
        erro: null
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
                body: JSON.stringify([filme.Id]),
                method: "DELETE",
                headers: {
                    "Content-Type": "application/json",
                    "Authorization" : "Bearer " + localStorage.token,                    
                },
            })
            .then(response => response.json())
            .then((data) => {
                if(data == "Sucesso"){
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
                    this.erro = null;
                } else
                    this.erro = "Não foi possível efetuar esta ação"
            });
        },
        criarFilme() {
            fetch(localStorage.link + "/api/Filme", {
                body: JSON.stringify(this.novoFilme),
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    "Authorization" : "Bearer " + localStorage.token,                    
                },
            })
            .then(response => response.json())
            .then((data) => {
                if(data == "Sucesso"){
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
                            this.novoFilme.Nome = "";
                            this.novoFilme.DateDeCriacao = "";
                            this.novoFilme.GeneroID = "";

                            this.erroCriarData = null;
                            this.erroCriarNome = null;
                            this.erroCriarGenero = null;
                        })
                    })
                } else {
                    if(data.ModelState) {
                        if(data.ModelState["filme.DateDeCriacao"])
                            this.erroCriarData = data.ModelState["filme.DateDeCriacao"][0];
                        else
                            this.erroCriarData = null;
                        if(data.ModelState["filme.Nome"])
                            this.erroCriarNome = data.ModelState["filme.Nome"][0];
                        else
                            this.erroCriarNome = null;
                        if(data.ModelState["filme.GeneroID"])
                            this.erroCriarGenero = "Informar genero correto";
                        else
                            this.erroCriarGenero = null;
                    } else {
                        this.erro = null;
                    }

                }
            })
        },
        getGenero(generoId){
            return this.generos.find(element => element.id == generoId).Nome
        },
        mudarSelecao(remover) {
            function changeRemoverStatus(e, i, a){
                e.Remover = remover
            };

            this.filmes.forEach(changeRemoverStatus);

        },
        removerSelecionados() {
            var listId = []
            debugger;
            function removerElemento(e, i, a){
                if(e.Remover) {
                    listId.push(e.Id);
                }
            };           

            this.filmes.forEach(removerElemento);

            fetch(localStorage.link + "/api/Filme", {
                body: JSON.stringify(listId),
                method: "Delete",
                headers: {
                    "Content-Type": "application/json",
                    "Authorization" : "Bearer " + localStorage.token,                    
                },
            })
            .then(response => response.json())
            .then((data) => {
                if(data == "Sucesso"){
                    fetch(localStorage.link + "/api/Filme", { 
                        headers : {
                            "Authorization" : "Bearer " + localStorage.token 
                        }
                    })
                    .then(response => response.json())
                    .then((data) => {
                        this.filmes = null;
                        this.filmes = data;
                        this.selecionarTodos = false;
                    })
                } else {
                    this.erro = "Não foi possível efetuar esta ação"
                }
            });
        }
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