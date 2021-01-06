<template>
  <div>
    <titulo
      :texto="`Aluno : ${aluno.nome} ${aluno.sobrenome}`"
      :btnVoltar="!visualizando"
    >
      <button v-show="visualizando" @click="editar()" class="btn btnEditar">
        Editar
      </button>
    </titulo>

    <table>
      <tbody>
        <tr>
          <td class="colPequeno">Matricula :</td>
          <td>
            <label>{{ aluno.id }}</label>
          </td>
        </tr>
        <tr>
          <td class="colPequeno">Nome:</td>
          <td>
            <label v-if="visualizando">{{ aluno.nome }}</label>
            <input v-else v-model="aluno.nome" type="text" />
          </td>
        </tr>
        <tr>
          <td class="colPequeno">Sobrenome:</td>
          <td>
            <label v-if="visualizando">{{ aluno.sobrenome }}</label>
            <input v-else v-model="aluno.sobrenome" type="text" />
          </td>
        </tr>
        <tr>
          <td class="colPequeno">Data Nascimento:</td>
          <td>
            <label v-if="visualizando">{{ aluno.dataNascimento }}</label>
            <input v-else v-model="aluno.dataNascimento" type="text" />
          </td>
        </tr>
        <tr>
          <td class="colPequeno">Professor:</td>
          <td>
            <label v-if="visualizando">{{ aluno.professor.nome }}</label>
            <select v-else v-model="aluno.professor">
              <option v-for="(professor, index) in professores"
                :key="index" :value="professor">
                {{professor.nome}}
              </option>
            </select>
          </td>
        </tr>
      </tbody>
    </table>
    <div style="margin-top: 10px">
      <div v-if="!visualizando">
        <button class="btn btnSalvar" @click="salvar(aluno)">Salvar</button>
        <button class="btn btnCancelar" @click="cancelar()">Cancelar</button>
      </div>
    </div>
  </div>
</template>

<script>
import Titulo from "../_share/Titulo";

export default {
  components: {
    Titulo,
  },

  data() {
    return {
      visualizando: true,
      professores: [],
      aluno: {},
      idAluno: this.$route.params.id,
    };
  },
  created() {
    this.$http
      .get("http://localhost:3000/alunos/" + this.idAluno)
      .then((res) => res.json())
      .then((aluno) => (this.aluno = aluno));

    this.$http
      .get("http://localhost:3000/professores") //pegou todos professores
      .then((res) => res.json())
      .then((professor) => {
        this.professores = professor;
      });
  },

  methods: {
    editar() {
      if (this.visualizando) {
        this.visualizando = false;
      }
    },
    salvar(aluno) {
      let _aluno = {
        id: aluno.id,
        nome: aluno.nome,
        sobrenome: aluno.sobrenome,
        dataNascimento: aluno.dataNascimento,
        professor: aluno.professor
      };
      this.$http
        .put(`http://localhost:3000/alunos/${ _aluno.id}`,
         _aluno);//Template string , coloco a informacao do aluno +id e  depois da virgula e o objeto to informacoes novas
        this.cancelar();
    },
    cancelar() {
      console.log(this.visualizando);
      if (this.visualizando == false) {
        this.visualizando = true;
      }
    },
  },
};
</script>


<style scoped>
.colPequeno {
  width: 20%;
}
input,
select {
  margin: 0px;
  padding: 5px 10px;
  font-size: 0.9em;
  border: 1px solid silver;
  border-radius: 5px;
  font-family: Montserrat;
  width: 95%;
  background-color: rgb(245, 245, 245);
}

.btnEditar {
  float: right;
  background-color: rgb(76, 186, 249);
}
.btnSalvar {
  float: right;
  background-color: rgb(79, 196, 68);
}
.btnCancelar {
  float: left;
  background-color: rgb(249, 186, 92);
}
</style>