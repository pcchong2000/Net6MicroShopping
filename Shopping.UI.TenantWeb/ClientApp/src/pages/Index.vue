<template>
  <div>
    <el-row  justify="center">
      <el-col :xs="20" :sm="16" :md="10" :lg="6" :xl="4">
        <div class="login-logo">
          
        </div>
      </el-col>
    </el-row> 
    <el-row  justify="center">
      <el-col :xs="20" :sm="16" :md="10" :lg="6" :xl="4">
        <div v-if="isLogin==0" class="login">
          <el-form ref="login" label-position="left" :model="loginInfo" label-width="80px">
            <el-form-item label="用户名">
              <el-input v-model="loginInfo.userName"></el-input>
            </el-form-item>
            <el-form-item label="密码">
              <el-input v-model="loginInfo.password"></el-input>
            </el-form-item>
            <el-form-item>
              <el-button type="primary" @click="login">登录</el-button>
              <el-link type="primary golink" @click="isLogin=1">去注册</el-link>
              <br/>
              <span class="login-error" v-if="loginResp.code!='success'">{{loginResp.message}}</span>
            </el-form-item>
          </el-form>
        </div>
        <div  v-if="isLogin==1"  class="register">
          <el-form ref="register"  label-position="left" :model="registerInfo" label-width="80px">
            <el-form-item label="商户名">
              <el-input v-model="registerInfo.tenantName"></el-input>
            </el-form-item>
            <el-form-item label="商户介绍">
              <el-input v-model="registerInfo.tenantDescription"></el-input>
            </el-form-item>
            <el-form-item label="商户编号">
              <el-input v-model="registerInfo.tenantCode"></el-input>
            </el-form-item>
            <el-form-item label="用户名">
              <el-input v-model="registerInfo.userName"></el-input>
            </el-form-item>
            <el-form-item label="绑定手机">
              <el-input v-model="registerInfo.phoneNumber"></el-input>
            </el-form-item>
            <el-form-item label="绑定邮箱">
              <el-input v-model="registerInfo.email"></el-input>
            </el-form-item>
            <el-form-item label="密码">
              <el-input v-model="registerInfo.password"></el-input>
            </el-form-item>
            <el-form-item label="确认密码">
              <el-input v-model="registerInfo.confirmPassword"></el-input>
            </el-form-item>
            <el-form-item label="验证码">
              <el-input v-model="registerInfo.code"></el-input>
            </el-form-item>
            <el-form-item>
              <el-button type="primary" @click="register">注册</el-button>
              <el-link type="primary golink" @click="isLogin=0">去登录</el-link>
            </el-form-item>
          </el-form>
        </div>
        <div  v-if="isLogin==2"  class="register-resp">
          <div>
            注册成功
          </div>
          <el-link type="primary golink" @click="isLogin=0">去登录</el-link>
        </div>
        <div  v-if="isLogin==3"  class="register-resp">
          <div>
            注册失败 {{registerResp.message}}
          </div>
          <el-link type="primary golink" @click="isLogin=1">重新注册</el-link>
        </div>
      </el-col>
    </el-row> 
  </div>
</template>

<script >
import  tenantService  from '../services/tenantService'
export default {
  name: 'Index',
  data:()=>{
    return {
      isLogin:0,
      loginInfo:{
        userName:"",
        password:"",
      },
      registerInfo:{
        "tenantName": "",
        "tenantDescription": "",
        "tenantCode": "",
        "userName": "",
        "phoneNumber": "",
        "email": "",
        "password": "",
        "code": ""
      },
      registerResp:{

      },
      loginResp:{}
    }
  },
  created(){
  },
  methods:{
    async register(){
      let registerResp= await tenantService.register(this.registerInfo);
      if(registerResp.code=="success"){
        this.isLogin=2;
      }else{
        this.isLogin=3;
        this.registerResp=registerResp;
      }
    },
    async login(){
      let loginData={
        userName:this.loginInfo.userName,
        password:this.loginInfo.password,
        grant_type:"password",
        client_id:oidc_config.client_id,
        client_secret:'',
      };
 
      tenantService.login(loginData).then(loginResp=>{
        this.loginResp=loginResp;
        if(loginResp.code=="success"){
          for (const key in loginResp) {
            localStorage.setItem(key,loginResp[key]);
          }
          this.$router.push("/index");
        }
      }).catch(error=>{
        this.loginResp=error.response.data;
      });
    },
  }
}
</script>

<style>
.login-logo{
height: 300px;
}
.login-error{
  color: red;
}
.golink{
  margin-left: 40px;
}
</style>
