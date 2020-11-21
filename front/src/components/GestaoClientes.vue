<template>
  <div class="hello">
    <div class="clientsList" v-if="this.exibirFormulario === false">
      <h1 class="mb">Gestão de Clientes</h1>

      <vs-table max-items="10" pagination :data="users">
        <template slot="thead">
          <vs-th> Nome </vs-th>
          <vs-th> Sobrenome </vs-th>
          <vs-th> CPF </vs-th>
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
        >Adicionar Cliente</vs-button
      >
    </div>
    <div class="form" v-else>
      <h1 id="label">Adicionar Clientes</h1>
      <vs-input label-placeholder="Nome" v-model="cliente.name" />
      <vs-input label-placeholder="Sobrenome" v-model="cliente.LastName" />
      <vs-input label-placeholder="CPF" v-model="cliente.cpf" />
      <vs-input label-placeholder="Telefone" v-model="cliente.PhoneNumber" />
      <vs-select class="phoneType" label="Tipo de Telefone" v-model="cliente.PhoneNumberType">
        <vs-select-item
          :key="index"
          :value="item.value"
          :text="item.text"
          v-for="(item, index) in options1"
        />
      </vs-select>
      <vs-input label-placeholder="Endereço" v-model="cliente.address" />
      <vs-select class="addressType" label="Tipo de Endereço" v-model="cliente.addressType">
        <vs-select-item
          :key="index"
          :value="item.value"
          :text="item.text"
          v-for="(item, index) in options2"
        />
      </vs-select>
      <vs-button
        @click.stop="enviarCliente()"
        color="success"
        type="filled"
        icon="add"
        >Adicionar</vs-button
      >
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
      options1:[
        {text:'Celular',value:"Celular"},
        {text:'Comercial',value:"Comercial"},
        {text:'Residencial',value:"Residencial"},
      ],
      options2:[
        {text:'Cobrança',value:"Cobrança"},
        {text:'Comercial',value:"Comercial"},
      ],
      cliente : {}
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
    },
    enviarCliente () {
      this.$http
        .post('http://localhost:49949/clientes/adicionarClientes', this.cliente)
        .then(() => {
          this.exibirFormulario = false
          this.cliente = {}
          this.carregarClientes()
        })
        .catch(function (error) {
          // handle error
          console.log(error);
        })
        .then(function () {
          // always executed
        });
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
  },
  mounted() {
    this.carregarClientes();
  },
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
