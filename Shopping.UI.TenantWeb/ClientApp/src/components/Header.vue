<template>
  <div>
    {{userInfo.name}}
    <el-button  type="info"  size="mini" @click="loginout" class="loginout">退出</el-button>
  </div>
</template>

<script >
//import  staffService  from '../services/staffService'
export default {
  name: 'Header',
  data:()=>{
    return {
      userInfo:{
        name:"张三",
        id:"",

      }
    };
  },
  async created(){
    if(!localStorage.getItem("access_token")){
      this.$router.push("/");
    }
  },
  methods:{
    loginout(){
      localStorage.clear();
      this.$router.push("/");
    }
  }
}
</script>

<style>
.loginout{
  float: right;
  margin-top: 15px;
}
</style>
