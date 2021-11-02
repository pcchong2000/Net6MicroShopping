<template>
  <div> Index</div>
</template>

<script >
import  orderService  from '../services/orderService'
export default {
  name: 'Index',
  created(){
    orderService.getList({}).then(a=>{
      console.log(a);
    });

  }
}
</script>

<style>

</style>
