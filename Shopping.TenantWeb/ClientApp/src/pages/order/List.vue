<template>
  <div> Staff List</div>
</template>

<script >
import  orderService  from '../../services/orderService'
export default {
  name: 'OrderList',
  created(){
    orderService.getList({}).then(a=>{
      console.log(a);
    });

  }
}
</script>

<style>

</style>
