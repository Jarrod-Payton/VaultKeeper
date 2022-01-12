<template>
  <header>
    <Navbar />
  </header>
  <main>
    <div class="row m-0 scrollbar background">
      <router-view />
    </div>
  </main>
  <footer></footer>
  <KeepModal />
  <CreateKeepModal />
  <CreateVaultModal />
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
export default {
  name: 'App',
  setup() {
    return {
      appState: computed(() => AppState)
    }
  }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";
</style>
