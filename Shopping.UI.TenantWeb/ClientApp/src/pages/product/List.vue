<template>
  <div>
    <div class="action">
      <el-button  type="info"  size="mini" @click="dialogVisible = true">添加</el-button>

    </div>
    <div class="data-table">
      <el-table :data="dataList" border style="width: 100%">
        <el-table-column prop="imageUrl" label="图片" width="180" />
        <el-table-column prop="name" label="名称" width="180" />
        <el-table-column prop="code" label="编号" width="180" />
        <el-table-column prop="price" label="价格" />
      </el-table>
      
    </div>
    <el-dialog v-model="dialogVisible" title="添加商品">
      <el-form ref="login" label-position="left" :model="productInfo" label-width="80px">
        <el-form-item label="商品名称">
          <el-input v-model="productInfo.name"></el-input>
        </el-form-item>
        <el-form-item label="编号">
          <el-input v-model="productInfo.code"></el-input>
        </el-form-item>
        <el-form-item label="图片">
          <el-input v-model="productInfo.imageUrl"></el-input>
        </el-form-item>
        <el-form-item label="价格">
          <el-input v-model="productInfo.price"></el-input>
        </el-form-item>
        <el-form-item label="排序">
          <el-input v-model="productInfo.sort"></el-input>
        </el-form-item>
        <el-form-item label="描述">
          <el-input v-model="productInfo.description"></el-input>
        </el-form-item>
        <el-form-item label="配置规格">
          <el-input v-model="productInfo.description"></el-input>
        </el-form-item>
      </el-form>
      <template #footer>
        <span class="dialog-footer">
          <el-button @click="dialogVisible = false">取消</el-button>
          <el-button type="primary" @click="addProduct">确定</el-button>
        </span>
      </template>
    </el-dialog>
  </div>
</template>

<script >
import  productService  from '../../services/productService'
export default {
  name: 'UserList',

  data:()=>{
    return {
      dataList:[],
      pageRequest:{
        pageIndex:1,
        pageSize:10,
      },
      dialogVisible:false,
      productInfo:{},
    };
  },
  created(){
    productService.getList(this.pageRequest).then(a=>{
      console.log(a);
      this.dataList=a.list;
    });

  },
  methods:{
    addProduct(){

      this.productInfo.storeId=localStorage.getItem("storeId");
      this.productInfo.storeName=localStorage.getItem("storeName");
      this.productInfo.status=1;
      this.productInfo.storeProductModelCategoryList=[];
      this.productInfo.storeProductModelList=[];

      productService.post(this.productInfo).then(resp=>{
        
      });

      this.dialogVisible=false;

    }
  }
}
</script>

<style scoped>
.action{
  margin-bottom: 10px;
}
</style>
