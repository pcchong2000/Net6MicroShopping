import  {createRouter,createWebHashHistory}  from 'vue-router'
// 定义一组路由
const routes = [
  { path: '/', component: () => import('../pages/Index.vue') },
  { path: '/login', component: () => import('../pages/Login.vue') },
  { path: '/logincallback', component: () => import('../pages/LoginCallback.vue') },
  { path: '/product/category/:id', component: () => import('../pages/ProductList.vue') },
  { path: '/product/:id', component: () => import('../pages/ProductDetail.vue') },
]
  
  //创建路由实例并传递 `routes` 配置
  //你可以在这里输入更多的配置，但我们在这里
  const router = createRouter({
    //内部提供了 history 模式的实现。为了简单起见，我们在这里使用 hash 模式。
    history: createWebHashHistory(),
    routes,
  })

  export default router;