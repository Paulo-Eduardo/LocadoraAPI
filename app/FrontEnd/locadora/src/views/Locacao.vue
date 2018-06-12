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
                        <button v-on:click="excluirGenero(genero)" class="btn-floating waves-effect waves-light red"><i  class="material-icons"> delete </i> </button>
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
                    </td>
                    <td>
                        <input type="text" v-model="novaLocacao.DataDeCriacao">
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
                    </td>
                </tr>
            </tbody>

            <!-- <tbody>
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
            </tbody> -->
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
        novoFilme: null
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
        criarLocacao() {
            fetch(localStorage.link + "/api/Locacao", {
                body: JSON.stringify(this.novaLocacao),
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    "Authorization" : "Bearer " + localStorage.token,                    
                },
            });
            this.novaLocacao = {
                CpfCliente: "",
                DataDeCriacao: "",
                Filmes: []
            }
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