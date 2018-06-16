<template>
  <div class="locacao">
     <div>
        <h3>Locacao</h3>
        <table class="striped">
            <thead>
                <tr>
                    <th width="120px">Ações</th>
                    <th width="400px">CPF</th>
                    <th width="400px">Data de Aluguel</th>
                </tr>
            </thead>
            <tbody v-for="locacao in this.locacoes" :key="locacao.Id" >
                <tr>
                    <td>
                        <button v-on:click="excluirLocacao(locacao)" class="btn-floating waves-effect waves-light red"><i  class="material-icons"> delete </i> </button>
                    </td>
                    <td>
                        {{locacao.CpfCliente}}
                    </td>
                    <td>
                        {{locacao.DataDeCriacao}}
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>                        
                        <table class="striped">
                            <tbody>
                                <tr>
                                    <td width = "250px">
                                        <b>Filmes Alugados</b>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr v-for="filme in locacao.Filmes" :key="filme.Id" >
                                    <td>
                                    </td>
                                    <td>
                                        {{filme.Nome}}
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </tbody>
            <tbody>
                <tr>
                    <td>
                        <button v-on:click="criarLocacao()" class="btn-floating waves-effect waves-light green"><i  class="material-icons"> add </i> </button>
                    </td>
                    <td>
                        <input type="text" class="validate" v-model="novaLocacao.CpfCliente">
                        <span v-if="erroCriarCpf"  class="helper-text">{{erroCriarCpf}}</span>
                    </td>
                    <td>
                        <input type="text" v-model="novaLocacao.DataDeCriacao">
                        <span v-if="erroCriarData"  class="helper-text">{{erroCriarData}}</span>
                    </td>
                </tr>
                <tr v-for="filme in this.novaLocacao.Filmes" :key="filme.id">
                    <td>
                        <button v-on:click="excluirNovoFilme(filme)" class="btn-floating waves-effect waves-light red"><i  class="material-icons"> delete </i> </button>
                    </td>
                    <td>
                        {{filme.Nome}}
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>                        
                        <select id="newFilme" class="browser-default" v-model="novoFilme">
                            <option></option>
                            <option v-for="filme in this.filmes" :key="filme.Id" :value="filme">{{filme.Nome}}</option>
                        </select>
                        <span v-if="erroCriarFilmes"  class="helper-text">{{erroCriarFilmes}}</span>
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
        filmes: null,
        locacoes: null,
        novaLocacao: {
            CpfCliente: "",
            DataDeCriacao: "",
            Filmes: []
        },
        novoFilme: null,
        erroCriarCpf: null,
        erroCriarFilmes: null,
        erroCriarData: null
      }
  },
  methods: {
        criarLocacao() {
            fetch(localStorage.link + "/api/Locacao", {
                body: JSON.stringify(this.novaLocacao),
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    "Authorization" : "Bearer " + localStorage.token,                    
                },
            })
            .then(response => response.json())
            .then((data) => {
                if(data == "Sucesso"){
                    fetch(localStorage.link + "/api/Locacao", { 
                            headers : {
                                "Authorization" : "Bearer " + localStorage.token 
                            }
                    })
                    .then(response => response.json())
                    .then((data) => {
                        this.locacoes = data;
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
                    this.novaLocacao = {
                    CpfCliente: "",
                    DataDeCriacao: "",
                    Filmes: []
                }
                } else {
                    if(data.ModelState["Locacao.CpfCliente"])
                        this.erroCriarCpf = data.ModelState["Locacao.CpfCliente"][0];
                    else
                        this.erroCriarCpf = null;
                    if(data.ModelState["Locacao.Filmes"])
                        this.erroCriarFilmes = data.ModelState["Locacao.Filmes"][0];
                    else
                        this.erroCriarFilmes = null;
                    if(data.ModelState["Locacao.DataDeCriacao"])
                        this.erroCriarData = "Informar data correta";
                    else
                        this.erroCriarData = null;
                }
            })
        },
        excluirLocacao(locacao) {
            fetch(localStorage.link + "/api/Locacao", {
                body: JSON.stringify(locacao),
                method: "DELETE",
                headers: {
                    "Content-Type": "application/json",
                    "Authorization" : "Bearer " + localStorage.token,                    
                },
            })            
            .then(() => {
                fetch(localStorage.link + "/api/Locacao", { 
                        headers : {
                            "Authorization" : "Bearer " + localStorage.token 
                        }
                })
                .then(response => response.json())
                .then((data) => {
                    this.locacoes = data;
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
            });
        },
        excluirNovoFilme(filme) {
            var index = this.novaLocacao.Filmes.indexOf(filme);
            this.novaLocacao.Filmes.splice(index, 1);
        }
    },
    mounted() {
        fetch(localStorage.link + "/api/Locacao", { 
                headers : {
                    "Authorization" : "Bearer " + localStorage.token 
                }
        })
        .then(response => response.json())
        .then((data) => {
            this.locacoes = data;
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
    },
    watch: {
        'novoFilme': function() {
            if(this.novoFilme != null && this.novoFilme != ""){
                this.novaLocacao.Filmes.push(this.novoFilme);
                this.novoFilme = "";
            }
        }
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

    /* table thead th { border-bottom: 1px solid #000; } */
</style>