<template>
  <div>
    
  </div>
</template>

<script >
//import  staffService  from '../services/staffService'
export default {
  name: 'Header',
  async created(){
    if(!localStorage.getItem("access_token")){
      this.$router.push("/");
    }
    //let current = await staffService.getStaffInfo();
  }
}
</script>

<style>

</style>
