<template>
  <div>
    <titulo
      :texto="`Professor : ${professor.nome} `"
      :btnVoltar="!visualizando"
    >
      <button v-show="visualizando" @click="editar()" class="btn btnEditar">
        Editar
      </button>
    </titulo>

    <table>
      <tbody>
        <tr>
          <td class="colPequeno">Matricula:</td>
          <td>
            <label>{{ professor.id }}</label>
          </td>
        </tr>
        <tr>
          <td class="colPequeno">Nome:</td>
          <td>
            <label v-if="visualizando">{{ professor.nome }}</label>
            <input v-else v-model="professor.nome" type="text" />
          </td>
        </tr>
        <tr>
          <td class="colPequeno">Materia:</td>
          <td>
            <label v-if="visualizando">{{ professor.materia }}</label>
            <input v-else v-model="professor.materia " type="text" />
          </td>
        </tr>
       
        
      </tbody>
    </table>
    <div style="margin-top: 10px">
      <div v-if="!visualizando">
        <button class="btn btnSalvar" @click="salvar(professor)">Salvar</button>
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
      professor: {},
      id: this.$route.params.professor_id,
      visualizando: true,
     
    };
  },
  created() {

    this.carregarProfessor();
  },

  methods: {
    carregarProfessor() {
        
   this.$http
      .get(`http://localhost:5000/api/professor/${this.id}`)//pegou todos professores 
      .then((res) => res.json())
      .then(professor => {
        this.professor = professor
      
        });
    
    
    },
    editar() {
      this.visualizando = !this.visualizando;
    },
  
  salvar(professor) {
      
    let _professor = {
      Id: professor.id,
      nome: professor.nome,
      materia: professor.materia,  
      alunos: []         
    };

    this.$http
      .put(`http://localhost:5000/api/professor/${_professor.Id}`, _professor)
      .then((res) => res.json())
      .then((professor) => (this.professor = professor))
      .then(() => (this.visualizando = true));
    this.visualizando = !this.visualizando;
  },
  cancelar() {
    this.visualizando = !this.visualizando;
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