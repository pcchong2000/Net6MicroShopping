<template>
  <div> User List</div>
</template>

<script >
import  storeService  from '../../services/storeService'
export default {
  name: 'UserList',
  data:()=>{
    return {
      storeList:[],
      pageRequest:{
        pageIndex:1,
        pageSize:10,
      },
    };
  },
  created(){
    storeService.getList(this.pageRequest).then(a=>{
      console.log(a);
    });

  },
  methods:{

  }
}
</script>

<style>

</style>
