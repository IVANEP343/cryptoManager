import { createRouter, createWebHistory } from 'vue-router';
import NewPurchaseFormView   from '../views/NewPurchaseFormView.vue';
import MovementHistoryView    from '../views/MovementHistoryView.vue';

const routes = [
  {
    path: '/new-purchase',
    name: 'NewPurchase',
    component: NewPurchaseFormView
  },
  {
    path: '/history',
    name: 'MovementHistory',
    component: MovementHistoryView
  }
];

export default createRouter({
  history: createWebHistory(),
  routes
});

