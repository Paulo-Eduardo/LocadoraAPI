<template>
  <div class="login">
        <span v-if="this.error" class="helper-text" data-error="wrong" data-success="right"> {{this.error}} </span>        
        <div class="input-field col s12">
          <input id="login" type="text" class="validate" placeholder="Login" v-model="login.userName">
        </div>
        <div class="input-field col s12">
          <input id="password" type="password" placeholder="Password" class="validate" v-model="login.password">
        </div>
        <div v-if="newLogin" class="input-field col s12">
          <input id="confirmPassword" type="password" placeholder="Confirm Password" class="validate" v-model="login.confirmPassword">
        </div>
        <button v-on:click="logar(login)" v-if="!newLogin" class="btn">Login</button> 
        <button v-on:click="createNew()" class="btn">Create</button>
  </div>
</template>

<script>
export default {
  name: 'Login',
  props: {
    msg: String
  },
  data(){
    return {
      newLogin: false,
      login: {
        userName: "",
        password: "",
        confirmPassword: ""
      },
      error: null
    }
  },
  methods: {
    createNew(){
      if(this.newLogin){
        fetch(localStorage.link + "/api/Account/Register",{
          body: JSON.stringify(this.login),
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
        })
        .then(response => response.json())
        .then((data) => {
            if(data.Succeeded != null){
              this.newLogin = false;
            }else if(data.ModelState[""] != null){
              this.error = data.ModelState[""][0];
            } else if(data.ModelState["userModel.Password"] != null) { 
              this.error = data.ModelState["userModel.Password"][0];  
            }else if(data.ModelState["userModel.ConfirmPassword"] != null) { 
              this.error = data.ModelState["userModel.ConfirmPassword"][0];  
            }
        })
      }else{
        this.newLogin = true
      }
    },
    logar(login) {
        fetch(localStorage.link + "/token",{
          method: "POST",
          headers: {
            "Content-Type": "application/x-www-form-urlencoded",
          },
          body: "userName="+login.userName+"&password="+login.password+"&grant_type=password"
        })
        .then(response => response.json())
        .then((data) => {
            if(data.error != null) { 
              this.error = data.error_description;  
            }
            else{         
              localStorage.token = data.access_token;
              this.$router.push('/Filme')
            }
        })
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
@import "https://cdnjs.cloudflare.com/ajax/libs/materialize/1.0.0-beta/css/materialize.min.css";
@import "https://cdnjs.cloudflare.com/ajax/libs/materialize/1.0.0-beta/js/materialize.min.js";

.login { 
  width:35%;
  margin: auto;
}
button {
  margin-right: 10px;
}
.error {
  text-align: left;
  color: red;
}

</style>