<template>
  <div> 
    <el-row>
      <el-col :span="12" :offset="6">
        <div class="grid-content bg-purple loginform">
          <el-form ref="form" :model="loginInfo" label-width="120px">
            <el-form-item label="用户名">
              <el-input v-model="loginInfo.userName"></el-input>
            </el-form-item>
            <el-form-item label="密码">
              <el-input v-model="loginInfo.password"></el-input>
            </el-form-item>
            
            <el-form-item>
              <el-button type="primary" @click="login">登录</el-button>
            </el-form-item>
          </el-form>
        </div>
        </el-col>
    </el-row>
    
  </div>
</template>

<script >
import oidcUserManager from '../common/oidc'
export default {
  name: 'Login',
  data:()=>{
    return {
      loginInfo:{
        userName:"",
        password:"",
      }
    }
  },
  methods:{
    login(){
      oidcUserManager.signinRedirect();
    },
  },
  created(){
    oidcUserManager.getUser().then((user)=> {
        if (user) {
            console.log("Login User:",user);
            this.$router.push("/store");
        }else {
            console.log("需要登陆");
        }
    });
  }
  
}
</script>

<style>

</style>
