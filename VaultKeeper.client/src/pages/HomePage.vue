<template>
  <div class="Home-Page" v-if="!loading">
    <div class="row m-0">
      <div class="col-12">
        <div class="container">
          <figure v-for="k in keeps" :key="k.id">
            <Keep :keep="k" />
          </figure>
        </div>
      </div>
    </div>
  </div>
  <Loading v-else />
</template>

<script>
import { computed } from "@vue/reactivity"
import { AppState } from "../AppState"
import { onMounted } from "@vue/runtime-core"
import { keepsService } from "../services/KeepsService"
import { resetService } from "../services/ResetService"
import { vaultsService } from "../services/VaultsService"
import Pop from "../utils/Pop"
import { logger } from "../utils/Logger"
export default {
  setup() {
    onMounted(async () => {
      try {
        AppState.loading = true
        await resetService.ResetEverything()
        await keepsService.GetAllKeeps()
        AppState.loading = false
      } catch (error) {
        Pop.toast(error.message, 'error')
        logger.error(error.message)
      }
    })
    return {
      keeps: computed(() => AppState.keeps),
      loading: computed(() => AppState.loading)
    }
  },
}
</script>

<style scoped lang="scss">
figure > img {
  grid-row: 1 / -1;
  grid-column: 1;
}
figure {
  margin: 0;
  display: grid;
  grid-template-rows: 1fr auto;
  margin-bottom: 10px;
  break-inside: avoid;
}
.container {
  column-count: 4;
  column-gap: 10px;
}
.home {
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;
  .home-card {
    width: 50vw;
    > img {
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
}
</style>
