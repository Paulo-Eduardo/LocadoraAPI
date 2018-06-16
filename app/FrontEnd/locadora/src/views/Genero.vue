<template>
  <div class="filme">
     <div>
        <h3>Gênero</h3>
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
                    <td>
                        <label>
                            <input type="checkbox" class="filled-in" v-model="genero.Remover"/>
                            <span></span>
                        </label>
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
                    <a v-on:click="removerSelecionados()" class="waves-effect waves-light btn"> Remover Selecionados </a>
                </tr>
                <tr>
                    <td>
                            <button v-on:click="criarGenero()" class="btn-floating waves-effect waves-light green"><i  class="material-icons"> add </i> </button>
                    </td>
                    <td>
                    </td>
                    <td>
                        <div class="input-field">
                            <input type="text" class="validate" v-model="novoGenero.Nome">
                            <span v-if="erroCriarNome"  class="helper-text">{{erroCriarNome}}</span>
                        </div>
                    </td>
                    <td>
                        <div class="input-field">
                            <input type="text" class="validate" v-model="novoGenero.DataDeCriacao">
                            <span v-if="erroCriarData"  class="helper-text">{{erroCriarData}}</span>
                        </div>
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
        generos : null,
        editandoGenero: null,
        novoGenero: {
            Nome: "",
            DataDeCriacao: "",
            Ativo: true,
        },
        erroCriarData: null,
        erroCriarNome: null,
        erro: null
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
            })
            .then(response => response.json())
            .then((data) => {
                if(data == "Sucesso"){
                    this.editandoGenero = null;
                } else
                    this.erro = "Não foi possível efetuar esta ação"
            });
        },
        excluirGenero(genero) {
            fetch(localStorage.link + "/api/Genero", {
                body: JSON.stringify([genero.Id]),
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
                    })
                } else
                    this.erro = "Não foi possível efetuar esta ação"
            });           
          
        },
        criarGenero() {
            fetch(localStorage.link + "/api/Genero", {
                body: JSON.stringify(this.novoGenero),
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
                        this.novoGenero = {
                            Nome: "",
                            DataDeCriacao: "",
                            Ativo: true,
                        }
                        this.erroCriarData = null;
                        this.erroCriarNome = null;
                    })
                } else {
                    if(data.ModelState) {
                        if(data.ModelState["Genero.DataDeCriacao"])
                            this.erroCriarData = data.ModelState["Genero.DataDeCriacao"][0];
                        else
                            this.erroCriarData = null;
                        if(data.ModelState["Genero.Nome"])
                            this.erroCriarNome = data.ModelState["Genero.Nome"][0];
                        else
                            this.erroCriarNome = null;
                    }
                }
            })
        },
        mudarSelecao(remover) {
            function changeRemoverStatus(e, i, a){
                e.Remover = remover
            };

            this.generos.forEach(changeRemoverStatus);

        },
        removerSelecionados() {
            var listId = []

            function removerElemento(e, i, a){
                if(e.Remover) {
                    listId.push(e.Id);
                }
            };           

            this.generos.forEach(removerElemento);

            fetch(localStorage.link + "/api/Genero", {
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
                    fetch(localStorage.link + "/api/Genero", { 
                        headers : {
                            "Authorization" : "Bearer " + localStorage.token 
                        }
                    })
                    .then(response => response.json())
                    .then((data) => {
                        this.generos = null;
                        this.generos = data;
                        this.selecionarTodos = false;
                    })
                } else
                    this.erro = "Não foi possível efetuar esta ação"
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