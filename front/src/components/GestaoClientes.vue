<template>
  <div class="hello">
    <div class="clientsList" v-if="this.exibirFormulario === false">
      <h1 class="mb">Gestão de Clientes</h1>

      <vs-table max-items="10" pagination :data="users" class="clientsList-tb">
        <template slot="thead">
          <vs-th> Nome </vs-th>
          <vs-th> Sobrenome </vs-th>
          <vs-th> CPF/CNPJ </vs-th>
          <vs-th> Telefone </vs-th>
          <vs-th> Tipo de Telefone </vs-th>
          <vs-th> Endereço </vs-th>
          <vs-th> Tipo de Endereço </vs-th>
          <vs-th> </vs-th>
        </template>

        <template slot-scope="{ data }">
          <vs-tr :key="indextr" v-for="(tr, indextr) in data">
            <vs-td :data="data[indextr].name">
              {{ data[indextr].name }}
            </vs-td>

            <vs-td :data="data[indextr].lastName">
              {{ data[indextr].lastName }}
            </vs-td>

            <vs-td :data="data[indextr].cpf">
              {{ data[indextr].cpf }}
            </vs-td>

            <vs-td :data="data[indextr].phoneNumber">
              {{ data[indextr].phoneNumber }}
            </vs-td>

            <vs-td :data="data[indextr].phoneNumberType">
              {{ data[indextr].phoneNumberType }}
            </vs-td>

            <vs-td :data="data[indextr].address">
              {{ data[indextr].address }}
            </vs-td>

            <vs-td :data="data[indextr].addressType">
              {{ data[indextr].addressType }}
            </vs-td>

            <vs-td>
              <vs-button
                @click.stop="editarCliente(tr)"
                color="dark"
                type="flat"
                icon="edit"
              ></vs-button>
              <vs-button
                @click.stop="removerCliente(tr)"
                color="danger"
                type="flat"
                icon="delete"
              ></vs-button>
            </vs-td>
          </vs-tr>
        </template>
      </vs-table>

      <vs-button
        @click.stop="adicionarCliente()"
        color="success"
        type="filled"
        icon="add"
        class="adicionarClient-bt"
        >Adicionar Cliente</vs-button
      >
    </div>
    <div class="form" v-else>
      <form>
        <h1 id="label">{{addTitle}}</h1>
        <vs-input label-placeholder="Nome" v-model="cliente.name" class="field"/>
        <vs-input label-placeholder="Sobrenome" v-model="cliente.lastName" class="field"/>
        <vs-input label-placeholder="CPF/CNPJ" v-model="cliente.cpf" v-mask="['###.###.###-##', '##.###.###/####-##']" class="field"/>
        <vs-input label-placeholder="Telefone" v-model="cliente.phoneNumber" v-mask="'(##) #####-####'" class="field"/>
        <vs-select
          class="field"
          label="Tipo de Telefone"
          v-model="cliente.phoneNumberType"
        >
          <vs-select-item
            :key="index"
            :value="item.value"
            :text="item.text"
            v-for="(item, index) in options1"
          />
        </vs-select>
        <vs-input label-placeholder="Endereço" v-model="cliente.address" class="field"/>
        <vs-select
          class="field"
          label="Tipo de Endereço"
          v-model="cliente.addressType"
        >
          <vs-select-item
            :key="index"
            :value="item.value"
            :text="item.text"
            v-for="(item, index) in options2"
          />
        </vs-select>
        <vs-alert class="back-bt" title="Aviso" :active.sync="exibirAlert" color="danger">
          Preencha todos os campos!
        </vs-alert>
        <div class="buttonsDiv">
          <vs-button
            @click.stop="enviarCliente()"
            color="success"
            type="filled"
            icon="add"
            class="add-bt"
            >{{btnEdit}}</vs-button
          >
          <vs-button
            @click.stop="home()"
            color="primary"
            type="filled"
            icon="keyboard_return"
            class="back-bt"
            >Voltar</vs-button
          >
        </div>
      </form>
    </div>
  </div>
</template>

<script>
export default {
  name: "GestaoClientes",
  data() {
    return {
      users: [],
      exibirFormulario: false,
      exibirAlert: false,
      options1: [
        { text: "Celular", value: "Celular" },
        { text: "Comercial", value: "Comercial" },
        { text: "Residencial", value: "Residencial" },
      ],
      options2: [
        { text: "Cobrança", value: "Cobrança" },
        { text: "Comercial", value: "Comercial" },
      ],
      cliente: {},
      addTitle: "Adicionar Cliente",
      btnEdit: "Adicionar"
    };
  },
  methods: {
    carregarClientes() {
      this.$http
        .get("http://localhost:49949/clientes/obterClientes")
        .then((response) => {
          this.users = response.data;
        })
        .catch(function (error) {
          // handle error
          console.log(error);
        })
        .then(function () {
          // always executed
        });
    },
    adicionarCliente() {
      this.exibirFormulario = true;
      this.addTitle = "Adicionar Cliente";
      this.btnEdit = "Adicionar";
    },
    enviarCliente() {
      if (!this.validarForm()) {
        this.exibirAlert = true
      } else {
        let requestName = "updateClientes"

        if(this.cliente.id === undefined)
          requestName = "adicionarClientes"


        this.$http
          .post(`http://localhost:49949/clientes/${requestName}`, this.cliente)
          .then(() => {
            this.exibirFormulario = false;
            this.exibirAlert = false;
            this.cliente = {};
            this.carregarClientes();
          })
          .catch(function (error) {
            // handle error
            console.log(error);
          })
          .then(function () {
            // always executed
          });
      }
    },
    home() {
      this.exibirFormulario = false;
      this.exibirAlert = false;
      this.cliente = {};
      },
    removerCliente(tr) {
      this.$http
        .post(`http://localhost:49949/clientes/deletarClientes/${tr.id}`)
        .then(() => {
          this.carregarClientes();
        })
        .catch(function (error) {
          // handle error
          console.log(error);
        })
        .then(function () {
          // always executed
        });
    },
    
    editarCliente(tr) {
      this.exibirFormulario = true;
      this.cliente = tr;
      this.addTitle = "Editar Cliente";
      this.btnEdit = "Salvar Alterações"
      
    },

    validarForm() {
      if(this.cliente.name === undefined || this.cliente.name === '')
        return false
      else if(this.cliente.lastName === undefined || this.cliente.lastName === '')
        return false
      else if(this.cliente.cpf === undefined || this.cliente.cpf === '')
        return false
      else if(this.cliente.phoneNumber === undefined || this.cliente.phoneNumber === '')
        return false
      else if(this.cliente.phoneNumberType === undefined || this.cliente.phoneNumberType === '')
        return false
      else if(this.cliente.address === undefined || this.cliente.address === '')
        return false
      else if(this.cliente.addressType === undefined || this.cliente.addressType === '')
        return false

      return true
    }
  },
  
  mounted() {
    this.carregarClientes();
  },
};

</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  .field {
    width: 100%;
  }

  .buttonsDiv {
    display: flex;
    justify-content: center;
  }

  .clientsList-tb {
    text-align: left;
    margin-left: 300px;
    margin-right: 300px; 
  }
  .mb {
    margin-bottom: 50px;
  }
  .adicionarClient-bt {
    margin-left: 300px;
  }

  /*Formulário*/
  .form {
    display: flex;
    flex-direction: column;
    align-items: center;
    margin-bottom: 20px;
  }

  #label {
    margin-bottom: 50px;
  }

  .add-bt {
    margin-top: 10px;
    margin-right: 10px;
  }

  .back-bt {
    margin-top: 10px;
  }
</style>
