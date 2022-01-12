<template>
  <div class="Home-Page" v-if="!loading">
    <div class="row m-0">
      <div class="col-md-4 col-sm-6 col-lg-3" v-for="k in keeps" :key="k.id">
        <Keep :keep="k" />
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
